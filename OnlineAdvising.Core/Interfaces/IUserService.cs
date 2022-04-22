using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserModel>> GetAsync(int id);
        Task<Result<List<UserModel>>> GetAsync();
        Task<Result<int>> UpdateAsync(UserModel model);
        Task<Result<UserModel>> AddAsync(UserModel model);
        Task<Result<UserModel>> LoginAsync(LoginModel model);
    }
}