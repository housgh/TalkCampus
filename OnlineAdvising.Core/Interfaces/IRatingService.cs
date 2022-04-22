using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IRatingService
    {
        Task<Result<RateModel>> GetAsync(int id);
        Task<Result<List<RateModel>>> GetAllAsync(int psychologistId);
        Task<Result<int>> AddAsync(RateModel model, int userId);
        Task<Result<double>> GetAverage(int psychologistId);
    }
}