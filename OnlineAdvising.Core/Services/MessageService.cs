using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message, int> _repository;
        private readonly IMapper _mapper;

        public MessageService(IRepository<Message, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<List<MessageModel>>> GetAllAsync(int chatId)
        {
            var messages = await _repository.FindAllAsync(x => x.ChatId == chatId);

            return new Result<List<MessageModel>>()
            {
                IsSucceeded = messages is not null,
                Value = messages is null ? null : _mapper.Map<List<MessageModel>>(messages)
            };
        }

        public async Task<Result<List<MessageModel>>> GetNewMessagesAsync(int chatId, int lastMessageId)
        {
            var newMessages = await _repository.DbContext.Messages
                .Where(x => x.ChatId == chatId && x.Id > lastMessageId).ToListAsync();
            
            foreach (var message in newMessages)
            {
                message.IsRead = true;
                await _repository.UpdateAsync(message);
            }

            return new Result<List<MessageModel>>()
            {
                IsSucceeded = newMessages is not null,
                Value = newMessages is null ? null : _mapper.Map<List<MessageModel>>(newMessages)
            };
        }

        public async Task<Result<MessageModel>> GetAsync(int messageId)
        {
            var messages = await _repository.FindAsync(x => x.Id == messageId);

            return new Result<MessageModel>()
            {
                IsSucceeded = messages is not null,
                Value = messages is null ? null : _mapper.Map<MessageModel>(messages)
            };
        }

        public async Task<Result<int>> AddAsync(MessageModel model)
        {
            var message = _mapper.Map<Message>(model);
            message.SentAt = DateTime.Now;
            var rowsAffected = await _repository.AddAsync(message);
            return new Result<int>()
            {
                IsSucceeded = rowsAffected > 0,
                Value = message.Id
            };
        }
    }
}