using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Data.Entities;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Data.Repositories
{
    public class PsychologistRepository : BaseRepository<Psychologist, int>, IPsychologistRepository
    {
        private readonly ApplicationDbContext _context;

        public PsychologistRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PsychologistDashboard> GetPsychologistDashboard(int psychologistId)
        {
            var dashboard = _context.PsychologistDashboard
                .FromSqlRaw($"EXEC GetPsychologistDashboard {psychologistId}")
                .AsEnumerable()
                .FirstOrDefault();
            return dashboard;
        }
    }
}