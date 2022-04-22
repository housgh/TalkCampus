using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Core.Interfaces
{
    public interface IMessageService
    {
        public Task<Result<List<MessageModel>>> GetAllAsync(int chatId);
        public Task<Result<MessageModel>> GetAsync(int messageId);
        public Task<Result<int>> AddAsync(MessageModel model);
        Task<Result<List<MessageModel>>> GetNewMessagesAsync(int chatId, int lastMessageId);
    }
}