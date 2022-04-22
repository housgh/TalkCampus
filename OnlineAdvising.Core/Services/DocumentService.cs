using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document, int> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<UserDocument, int> _userDocumentRepo;

        public DocumentService(
            IRepository<Document, int> repository,
            IRepository<UserDocument, int> userDocumentRepo,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userDocumentRepo = userDocumentRepo;
        }
        
        public async Task<Result<DocumentModel>> GetAsync(int id)
        {
            var document = await _repository.GetAsync(id);

            return new Result<DocumentModel>()
            {
                IsSucceeded = document is not null,
                Value = document is null ? null : _mapper.Map<DocumentModel>(document)
            };
        }

        public async Task<Result<List<DocumentModel>>> GetPerPsychologistAsync(int psychologistId)
        {
            var psychologistDocuments = await _userDocumentRepo.FindAllAsync(doc => doc.UserId == psychologistId);
            var psychologistDocumentIds = psychologistDocuments.Select(doc => doc.DocumentId); 
            var document = await _repository.FindAllAsync(doc => psychologistDocumentIds.Contains(doc.Id));

            return new Result<List<DocumentModel>>()
            {
                IsSucceeded = document is not null,
                Value = document is null ? null : _mapper.Map<List<DocumentModel>>(document)
            };
        }

        public async Task<Result<int>> AddAsync(int userId ,List<DocumentModel> model)
        {
            var documents = _mapper.Map<List<Document>>(model)
                .Select(document => new UserDocument
                {
                    UserId = userId,
                    Document = document
                });
            
            var documentsAdded = await _userDocumentRepo.AddRangeAsync(documents);

            return new Result<int>()
            {
                IsSucceeded = documentsAdded > 0,
                Value = documentsAdded
            };
        }
    }
}