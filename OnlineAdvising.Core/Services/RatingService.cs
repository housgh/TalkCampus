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
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rate, int> _repository;
        private readonly IMapper _mapper;

        public RatingService(IRepository<Rate, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<RateModel>> GetAsync(int id)
        {
            var rate = await _repository.GetAsync(id);

            return new Result<RateModel>()
            {
                IsSucceeded = rate is not null,
                Value = rate is null ? null : _mapper.Map<RateModel>(rate)
            };
        }

        public async Task<Result<List<RateModel>>> GetAllAsync(int psychologistId)
        {
            var rates = await _repository.FindAllAsync(rate => rate.RatedId == psychologistId);

            return new Result<List<RateModel>>()
            {
                IsSucceeded = rates is not null,
                Value = rates is null ? null : _mapper.Map<List<RateModel>>(rates)
            };
        }

        public async Task<Result<int>> AddAsync(RateModel model, int userId)
        {
            var ratedId = await _repository.DbContext.Chats.Where(x => x.Id == model.ChatId).Select(x =>
                x.Appointment.PsychologistId).FirstOrDefaultAsync();
            var rate = new Rate()
            {
                RatedId = ratedId,
                RateValue = model.RateValue,
            };
            var ratesAdded = await _repository.AddAsync(rate);

            return new Result<int>()
            {
                IsSucceeded = ratesAdded > 0,
                Value = ratesAdded
            };
        }

        public async Task<Result<double>> GetAverage(int psychologistId)
        {
            var rates = await _repository.FindAllAsync(rate => rate.RatedId == psychologistId);
            var average = rates?.Sum(rate => rate.RateValue) ?? 0;
            average /= rates?.Count > 0 ? rates.Count : 1;

            return new Result<double>()
            {
                IsSucceeded = rates is not null,
                Value = average
            };
        }
    }
}