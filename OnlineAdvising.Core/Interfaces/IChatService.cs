using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IChatService
    {
        public Task<Result<List<ChatModel>>> GetAllAsync(int userId);
        public Task<Result<ChatModel>> GetAsync(int chatId);
        public Task<Result<int>> AddAsync(ChatModel model);
    }
}