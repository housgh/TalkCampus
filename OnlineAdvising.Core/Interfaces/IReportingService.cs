using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IReportingService
    {
        Task<Result<ReportModel>> GetAsync(int id);
        Task<Result<List<ReportModel>>> GetAllAsync(int psychologistId);
        Task<Result<int>> AddAsync(ReportModel model, int reporterId);
    }
}