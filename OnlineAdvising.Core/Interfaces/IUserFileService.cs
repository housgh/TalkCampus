using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IUserFileService
    {
        Task<Result<List<UserFileModel>>> GetFiles();
        Task<Result<int>> UploadFiles(IFormFileCollection files, int userId);
        Task<Result<UserFileModel>> GetFile(int id);
    }
}