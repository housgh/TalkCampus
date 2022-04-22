using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment, int> _repository;
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public AppointmentService(IRepository<Appointment, int> repository, IMapper mapper, IChatService chatService)
        {
            _repository = repository;
            _mapper = mapper;
            _chatService = chatService;
        }

        public async Task<Result<AppointmentModel>> GetAsync(int id)
        {
            var appointment = await _repository.GetAsync(id);

            return new Result<AppointmentModel>()
            {
                IsSucceeded = appointment is not null,
                Value = appointment is null ? null : _mapper.Map<AppointmentModel>(appointment)
            };
        }

        public async Task<Result<List<AppointmentModel>>> GetPerUserAsync(int userId)
        {
            var appointments = await _repository.FindAllAsync(app =>
                (app.PsychologistId == userId || app.PatientId == userId) && app.StartDate >= DateTime.Now &&
                app.TimeEnded >= DateTime.Now && app.AppointmentStatusId != 2);

            return new Result<List<AppointmentModel>>()
            {
                IsSucceeded = appointments is not null,
                Value = appointments is null ? null : _mapper.Map<List<AppointmentModel>>(appointments)
            };
        }

        public async Task<Result<int>> UpdateAsync(AppointmentModel model)
        {
            var appointment = _mapper.Map<Appointment>(model);
            var appointmentsUpdated = await _repository.UpdateAsync(appointment);

            return new Result<int>()
            {
                IsSucceeded = appointmentsUpdated > 0,
                Value = appointmentsUpdated
            };
        }

        public async Task<Result<int>> AddAsync(AppointmentModel model)
        {
            var appointment = _mapper.Map<Appointment>(model);
            appointment.EndDate = appointment.StartDate.AddHours(1);
            appointment.TimeEnded = DateTime.Now.AddYears(20);
            appointment.PsychologistId = await GetAvailablePsychologist(appointment.StartDate);
            if (appointment.PsychologistId == null)
            {
                return new Result<int>()
                {
                    IsSucceeded = false
                };
            }

            appointment.EndDate = appointment.StartDate.AddHours(1);
            var appointmentsAdded = await _repository.AddAsync(appointment);
            var chat = new ChatModel()
            {
                AppointmentId = appointment.Id
            };
            var chatsResult = await _chatService.AddAsync(chat);
            return new Result<int>()
            {
                IsSucceeded = appointmentsAdded > 0 && chatsResult.Value > 0,
                Value = appointmentsAdded
            };
        }

        public async Task<Result<int>> GetHoursServed(int psychologistId)
        {
            var hours = await _repository.DbContext.Appointments
                .Where(x => x.PsychologistId == psychologistId)
                .SumAsync(x => x.EndDate.Hour - x.StartDate.Hour);

            return new Result<int>()
            {
                IsSucceeded = true,
                Value = hours
            };
        }

        public async Task<Result<int>> GetAppointmentsCount(int psychologistId)
        {
            var count = await _repository.DbContext.Appointments
                .Where(x => x.PsychologistId == psychologistId).CountAsync();

            return new Result<int>()
            {
                IsSucceeded = true,
                Value = count
            };
        }

        private async Task<int?> GetAvailablePsychologist(DateTime appointmentTime)
        {
            return await _repository.DbContext.ScheduleDays
                .Include(x => x.Schedule)
                .Where(x =>
                    x.DayOfWeekId.Value == (int) appointmentTime.DayOfWeek
                    && x.StartHour <= appointmentTime.Hour && x.EndHour >= appointmentTime.Hour
                    && !_repository.DbContext.Appointments.Any(t => t.StartDate.Hour == appointmentTime.Hour
                                                                    && t.PsychologistId == x.Schedule.PsychologistId))
                .Select(x => x.Schedule.PsychologistId).FirstOrDefaultAsync();
        }

        public async Task<Result<int>> DeclineAppointment(int id)
        {
            var appointment = await _repository.GetAsync(id);
            appointment.AppointmentStatusId = 2;
            var rowsAffected = await _repository.UpdateAsync(appointment);
            return new Result<int>()
            {
                IsSucceeded = rowsAffected >= 1,
                Value = rowsAffected
            };
        }

        public async Task<Result<int>> EndAppointment(int id)
        {
            var appointment = await _repository.FindAsync(x =>
                _repository.DbContext.Chats.Any(y => y.AppointmentId == x.Id && y.Id == id));
            appointment.TimeEnded = DateTime.Now;
            var rowsAffected = await _repository.UpdateAsync(appointment);
            return new Result<int>()
            {
                IsSucceeded = rowsAffected >= 1,
                Value = rowsAffected
            };
        }
    }
}