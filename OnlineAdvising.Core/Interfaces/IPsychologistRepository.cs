using System.Threading.Tasks;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IPsychologistRepository : IRepository<Psychologist, int>
    {
        Task<PsychologistDashboard> GetPsychologistDashboard(int psychologyId);
    }
}