using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContentService:IContentService
    {
        private readonly IContentRepository _contentRepository;
        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task CreateAsync(Content content)
        {
            await _contentRepository.CreateAsync(content);
        }

        public async Task DeleteAsync(Content content)
        {
           await  _contentRepository.DeleteAsync(content);
        }

        public async Task<IEnumerable<Content>> GetAllAsync()
        {
           return await _contentRepository.GetAllAsync();
        }

        public async Task<Content> GetByIdAsync(int id)
        {
            return await _contentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Content content)
        {
            await _contentRepository.UpdateAsync(content);
        }
    }
}
