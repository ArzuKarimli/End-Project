
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;
using Service.ViewModel;

namespace Asp_project.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
  
    using System.Linq;
    using System.Threading.Tasks;

    namespace Service.ViewComponents
    {
        public class SliderViewComponent : ViewComponent
        {
            private readonly ISliderService _sliderService;
            private readonly ISliderInfoService _sliderInfoService;

            public SliderViewComponent(ISliderService sliderService, ISliderInfoService sliderInfoService)
            {
                _sliderService = sliderService;
                _sliderInfoService = sliderInfoService;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var sliders = await _sliderService.GetAllAsync();
                var sliderInfos = await _sliderInfoService.GetAllAsync();

                var viewModel = new SliderVMVC
                {
                    Sliders = sliders.ToList(),
                    SliderInfos = sliderInfos.ToList()
                };

                return View(viewModel);
            }
        }
    }



}
