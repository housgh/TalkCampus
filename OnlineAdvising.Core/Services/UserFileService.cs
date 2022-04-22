using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;
using OnlineAdvising.Data.Entities;

namespace OnlineAdvising.Core.Services
{
    public class UserFileService : IUserFileService
    {
        private readonly IRepository<UserFile, int> _repository;
        private readonly IMapper _mapper;

        public UserFileService(IRepository<UserFile, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<UserFileModel>>> GetFiles()
        {
            var files = _repository.GetAsync();
            return new Result<List<UserFileModel>>()
            {
                IsSucceeded = files is not null,
                Value = files is null ? null : _mapper.Map<List<UserFileModel>>(files)
            };
        }

        public async Task<Result<int>> UploadFiles(IFormFileCollection filesCollection, int userId)
        {
            var files = new List<UserFile>();
            foreach (var file in filesCollection)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                var userFile = new UserFile()
                {
                    Name = string.Concat(string.Concat(fileName[..fileName.IndexOf(".")],
                        Guid.NewGuid().ToString()), fileExtension),
                    UserId = userId,
                };
                using var target = new MemoryStream();
                file.CopyTo(target);
                userFile.Value = target.ToArray();
                files.Add(userFile);
            }

            var rowsAdded = await _repository.AddRangeAsync(files);
            return new Result<int>()
            {
                IsSucceeded = rowsAdded > 0,
                Value = rowsAdded
            };
        }

        public async Task<Result<UserFileModel>> GetFile(int id)
        {
            var file = await _repository.GetAsync(id);
            return new Result<UserFileModel>()
            {
                IsSucceeded = file is not null,
                Value = file is null ? null : _mapper.Map<UserFileModel>(file)
            };
        }
    }
}