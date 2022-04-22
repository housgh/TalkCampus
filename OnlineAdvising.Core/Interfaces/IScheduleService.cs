using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IScheduleService
    {
        Task<Result<ScheduleModel>> GetAsync(int id);
        Task<Result<List<ScheduleModel>>> GetAsync();
        Task<Result<int>> UpdateAsync(ScheduleModel model);
        Task<Result<int>> AddAsync(ScheduleModel model);
    }
}