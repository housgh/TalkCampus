using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, int> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UserModel>> GetAsync(int id)
        {
            var user = await _repository.GetAsync(id);

            var result = new Result<UserModel>()
            {
                IsSucceeded = user is not null,
            };
            if (!result.IsSucceeded) return result;
            var userModel = _mapper.Map<UserModel>(user);
            var upcomingAppointmentId =
                await _repository.DbContext.Appointments.Where(x =>
                        x.PatientId == id && x.StartDate.Date == DateTime.Now.Date &&
                        x.StartDate.Hour == DateTime.Now.Hour && x.TimeEnded >= DateTime.Now &&
                        x.AppointmentStatusId != 2)
                    .Select(x => x.Id).FirstOrDefaultAsync();
            if (upcomingAppointmentId != 0)
            {
                userModel.HasAppointment = true;
                userModel.UpcomingChatId = await _repository.DbContext.Chats
                    .Where(x => x.AppointmentId == upcomingAppointmentId).Select(x => x.Id).FirstOrDefaultAsync();
            }

            result.Value = userModel;
            return result;
        }

        public async Task<Result<AdminDashboard>> GetAdminDashboardUsers()
        {
            var psychologists = await _repository.DbContext.AdminDashboardPsychologists
                .FromSqlRaw("EXEC GetAdminDashboardPsychologists").ToListAsync();
            var patients = await _repository.DbContext.AdminDashboardPatients
                .FromSqlRaw("EXEC GetAdminDashboardPatients").ToListAsync();
        
            foreach (var psychologist in psychologists)
            {
                var files = await _repository.DbContext.UserFiles.Where(x => x.UserId == psychologist.Id)
                    .ToListAsync();
                psychologist.Files = files;
            }
            
            return new Result<AdminDashboard>()
            {
                IsSucceeded = psychologists is not null && patients is not null,
                Value = new AdminDashboard()
                {
                    Psychologists = psychologists,
                    Patients = patients
                }
            };
        }

        public async Task<Result<UserModel>> AddAsync(UserModel model)
        {
            User user = null;
            user = model.RoleId == 1?  _mapper.Map<Psychologist>(model) : _mapper.Map<Patient>(model);
            user.AccountStatusId = model.RoleId == 1 ? 3 : 1;
            if (await _repository.DbContext.Users.AnyAsync(x => x.Username == user.Username))
            {
                return new Result<UserModel>()
                {
                    IsSucceeded = false,
                    Error = "Username Already Exists"
                };
            }
            
            if(await _repository.DbContext.Users.AnyAsync(x => x.Email == user.Email))
            {
                return new Result<UserModel>()
                {
                    IsSucceeded = false,
                    Error = "Email Already Exists"
                };
            }
            user.PasswordHash = HashPassword(model.Password);
            var usersAdded = await _repository.AddAsync(user);
            user = await _repository.DbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            return new Result<UserModel>()
            {
                IsSucceeded = user is not null,
                Value = _mapper.Map<UserModel>(user)
            };
        }

        public async Task<Result<UserModel>> LoginAsync(LoginModel model)
        {
            var passwordHash = HashPassword(model.Password);
            var user = await _repository.DbContext.Users.FirstOrDefaultAsync(x =>
                x.Username == model.Username && x.PasswordHash == passwordHash);
            return new Result<UserModel>()
            {
                IsSucceeded = user is not null,
                Value = user is null ? null : _mapper.Map<UserModel>(user)
            };
        }

        public async Task<Result<int>> ActivatePsychologist(int id)
        {
            var psychologist = await _repository.GetAsync(id);
            if (psychologist is null) return new Result<int>();
            psychologist.AccountStatusId = 1;
            var rowsAffected = await _repository.UpdateAsync(psychologist);
            return new Result<int>()
            {
                IsSucceeded = rowsAffected > 0,
                Value = rowsAffected
            };
        }

        private string HashPassword(string password)
        {
            byte[] GetHash(string password)
            {
                using (HashAlgorithm algorithm = SHA256.Create())
                    return algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var stringBuilder = new StringBuilder();
            foreach (var b in GetHash(password))
                stringBuilder.Append(b.ToString("X2"));

            return stringBuilder.ToString();
        }
    }
}