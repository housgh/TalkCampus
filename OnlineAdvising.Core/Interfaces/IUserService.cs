using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.ProcedureModels;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserModel>> GetAsync(int id);
        Task<Result<AdminDashboard>> GetAdminDashboardUsers();
        Task<Result<UserModel>> AddAsync(UserModel model);
        Task<Result<UserModel>> LoginAsync(LoginModel model);
        Task<Result<int>> ActivatePsychologist(int id);
    }
}