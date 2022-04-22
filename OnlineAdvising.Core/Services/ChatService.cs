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
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat, int> _chatRepository;
        private readonly IMapper _mapper;

        public ChatService(
            IRepository<Chat, int> chatRepository,
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ChatModel>>> GetAllAsync(int userId)
        {
            var chats = await _chatRepository.DbContext.Chats.Include(x => x.Appointment)
                .Where(x => (x.Appointment.PatientId == userId || x.Appointment.PsychologistId == userId) 
                            && x.Appointment.EndDate < DateTime.Now)
                .OrderByDescending(x => x.Appointment.StartDate)
                .ToListAsync();
            return new Result<List<ChatModel>>()
            {
                IsSucceeded = chats is not null,
                Value = chats is null ? null : _mapper.Map<List<ChatModel>>(chats)
            };
        }

        public async Task<Result<ChatModel>> GetAsync(int chatId)
        {
            var chat = await _chatRepository.FindAsync(x => x.Id == chatId);

            return new Result<ChatModel>()
            {
                IsSucceeded = chat is not null,
                Value = chat is null ? null : _mapper.Map<ChatModel>(chat)
            };
        }

        public async Task<Result<int>> AddAsync(ChatModel model)
        {
            var chat = _mapper.Map<Chat>(model);
            var rowsAffected = await _chatRepository.AddAsync(chat);

            return new Result<int>()
            {
                IsSucceeded = rowsAffected > 0,
                Value = rowsAffected
            };
        }
    }
}