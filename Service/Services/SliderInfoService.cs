
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderInfoService : ISliderInfoService
    {
       
        private readonly ISliderInfoRepository _sliderInfoRepository;

        public SliderInfoService( ISliderInfoRepository sliderInfoRepository)
        {
        
            _sliderInfoRepository = sliderInfoRepository;
        }

        public async Task<IEnumerable<SliderInfo>> GetAllAsync()
        {
          return await _sliderInfoRepository.GetAllAsync();
        }
    }


}

