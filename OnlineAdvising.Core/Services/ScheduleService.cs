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
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<PsychologistSchedule, int> _repository;
        private readonly IMapper _mapper;

        public ScheduleService(IRepository<PsychologistSchedule, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Result<ScheduleModel>> GetAsync(int psychologistId)
        {
            var schedule = await _repository.DbContext.PsychologistSchedules.Include(x => x.ScheduleDays)
                .FirstOrDefaultAsync(x => x.PsychologistId == psychologistId);

            return new Result<ScheduleModel>()
            {
                IsSucceeded = schedule is not null,
                Value = schedule is null ? null : _mapper.Map<ScheduleModel>(schedule)
            };
        }

        public async Task<Result<List<ScheduleModel>>> GetAsync()
        {
            var schedules = await _repository.GetAsync();
            return new Result<List<ScheduleModel>>()
            {
                IsSucceeded = schedules is not null,
                Value = schedules is null ? null : _mapper.Map<List<ScheduleModel>>(schedules)
            };
        }

        public async Task<Result<int>> UpdateAsync(ScheduleModel model)
        {
            var rowsAffected = await _repository.UpdateAsync(_mapper.Map<PsychologistSchedule>(model));
            return new Result<int>()
            {
                IsSucceeded = rowsAffected > 0,
                Value = rowsAffected
            };
        }

        public async Task<Result<int>> AddAsync(ScheduleModel model)
        {
            var rowsAffected = await _repository.AddAsync(_mapper.Map<PsychologistSchedule>(model));
            return new Result<int>()
            {
                IsSucceeded = rowsAffected > 0,
                Value = rowsAffected
            };
        }
    }
}