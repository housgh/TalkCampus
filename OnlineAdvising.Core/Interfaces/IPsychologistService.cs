using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IPsychologistService
    {
        Task<Result<PsychologistModel>> GetAsync(int id);
        Task<Result<List<PsychologistModel>>> GetAsync();
        Task<Result<int>> UpdateAsync(PsychologistModel model);
        Task<Result<int>> AddAsync(PsychologistModel model);
        Task<Result<PsychologistDashboard>> GetPsychologistDashboard(int psychologistId);
    }
}