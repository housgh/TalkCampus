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
    public class ReportingService : IReportingService
    {
        private readonly IRepository<Report, int> _repository;
        private readonly IMapper _mapper;

        public ReportingService(IRepository<Report, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }        
        
        public async Task<Result<ReportModel>> GetAsync(int id)
        {
            var report = await _repository.GetAsync(id);

            return new Result<ReportModel>()
            {
                IsSucceeded = report is not null,
                Value = report is null ? null : _mapper.Map<ReportModel>(report)
            };
        }

        public async Task<Result<List<ReportModel>>> GetAllAsync(int psychologistId)
        {
            var reports = await _repository.FindAllAsync(report => report.ReportedId == psychologistId);

            return new Result<List<ReportModel>>()
            {
                IsSucceeded = reports is not null,
                Value = reports is null ? null : _mapper.Map<List<ReportModel>>(reports)
            };
        }

        public async Task<Result<int>> AddAsync(ReportModel model, int userId)
        {
            var reportedId = await _repository.DbContext.Chats.Where(x => x.Id == model.ChatId).Select(x =>
                x.Appointment.PatientId != userId ? x.Appointment.PatientId : x.Appointment.PsychologistId).FirstOrDefaultAsync();
            var report = new Report()
            {
                ReportedId = reportedId,
                Reason = model.Reason
            };
            var reportsAdded = await _repository.AddAsync(report);

            return new Result<int>()
            {
                IsSucceeded = reportsAdded > 0,
                Value = reportsAdded
            };
        }
    }
}