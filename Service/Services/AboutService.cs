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
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task CreateAsync(About model)
        {
            await _aboutRepository.CreateAsync(model);
        }

        public async Task DeleteAsync(About model)
        {
          await _aboutRepository.DeleteAsync(model);
        }

        public  async Task<IEnumerable<About>> GetAllAsync()
        {
           return await _aboutRepository.GetAllAsync(); 
        }

        public async Task<About> GetByIdAsync(int id)
        {
           return await _aboutRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(About model)
        {
           await _aboutRepository.UpdateAsync(model);
        }
    }
}
