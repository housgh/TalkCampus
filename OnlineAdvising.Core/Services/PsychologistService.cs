using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Core.Services
{
    public class PsychologistService : IPsychologistService
    {
        private readonly IPsychologistRepository _repository;
        private readonly IMapper _mapper;

        public PsychologistService(IPsychologistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Result<PsychologistModel>> GetAsync(int id)
        {
            var psychologist = await _repository.GetAsync(id);
            
            return new Result<PsychologistModel>()
            {
                IsSucceeded = psychologist is not null,
                Value = psychologist is null ? null : _mapper.Map<PsychologistModel>(psychologist)
            };
        }

        public async Task<Result<List<PsychologistModel>>> GetAsync()
        {
            var psychologists = await _repository.FindAllAsync();

            return new Result<List<PsychologistModel>>()
            {
                IsSucceeded = psychologists is not null,
                Value = psychologists is null? null : _mapper.Map<List<PsychologistModel>>(psychologists)
            };
        }

        public async Task<Result<int>> UpdateAsync(PsychologistModel model)
        {
            var psychologist = _mapper.Map<Psychologist>(model);
            var psychologistsUpdated = await _repository.UpdateAsync(psychologist);

            return new Result<int>()
            {
                IsSucceeded = psychologistsUpdated > 0,
                Value = psychologistsUpdated
            };
        }

        public async Task<Result<int>> AddAsync(PsychologistModel model)
        {
            var psychologist = _mapper.Map<Psychologist>(model);
            var psychologistAdded = await _repository.AddAsync(psychologist);

            return new Result<int>()
            {
                IsSucceeded = psychologistAdded > 0,
                Value = psychologistAdded
            };
        }

        public async Task<Result<PsychologistDashboard>> GetPsychologistDashboard(int psychologistId)
        {
            var dashboard = await _repository.GetPsychologistDashboard(psychologistId);
            return new Result<PsychologistDashboard>()
            {
                IsSucceeded = dashboard is not null,
                Value = dashboard
            };
        }
    }
}