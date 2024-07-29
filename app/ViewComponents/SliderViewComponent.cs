
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.Services;
using Service.Services.Interfaces;

namespace Asp_project.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderInfoService _sliderInfoService;

        public SliderViewComponent(ISliderInfoService sliderInfoService)
        {
            _sliderInfoService=sliderInfoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _sliderInfoService.GetAllAsync();
            return View(model);
        }
    }


}
