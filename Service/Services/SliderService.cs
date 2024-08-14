using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;



namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
       

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task AddAsync(Slider slider)
        {
            await _sliderRepository.CreateAsync(slider);
           
        }

        public async Task DeleteSliderAsync(Slider slider)
        {
            await _sliderRepository.DeleteAsync(slider);
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
           return await _sliderRepository.GetAllAsync();
        }

        public async Task<Slider> GetByIdForSliderAsync(int id)
        {
           return await _sliderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Slider>> GetAllWithInfoAsync()
        {
           return await _sliderRepository.GetAllWithInfoAsync();
        }

        

        public async Task<Slider> GetByIdWithInfoAsync(int id)
        {
            return await _sliderRepository.GetByIdWithInfoAsync(id);
        }
        public async Task UpdateAsync(Slider slider)
        {
            await _sliderRepository.UpdateAsync(slider);
        }

    }


}
