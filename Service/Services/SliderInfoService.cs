using app.ViewModel;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly ISliderInfoRepository _sliderInfoRepository;

        public SliderInfoService(ISliderRepository sliderRepository, ISliderInfoRepository sliderInfoRepository)
        {
            _sliderRepository = sliderRepository;
            _sliderInfoRepository = sliderInfoRepository;
        }

        public async Task<IEnumerable<SliderVMVC>> GetAllAsync()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            var sliderInfos = await _sliderInfoRepository.GetAllAsync();

           
            return sliders.Select(slider =>
            {
                var info = sliderInfos.FirstOrDefault(si => si.SliderId == slider.Id);
                return new SliderVMVC
                {
                    Image = slider.Image,
                    Title = info?.Title,
                    Description = info.Description
                };
            }).ToList();
        }
    }


}

