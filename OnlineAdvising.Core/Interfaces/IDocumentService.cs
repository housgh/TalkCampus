using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IDocumentService
    {
        Task<Result<DocumentModel>> GetAsync(int id);
        Task<Result<List<DocumentModel>>> GetPerPsychologistAsync(int psychologistId);
        Task<Result<int>> AddAsync(int userId, List<DocumentModel> model);
    }
}