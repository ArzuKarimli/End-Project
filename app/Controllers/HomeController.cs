
using AutoMapper.Features;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace app.Controllers
{
    public class HomeController : Controller
    {
      private readonly IContentService _contentService;
        private readonly IAboutService _aboutService;
        public HomeController(IContentService contentService, IAboutService aboutService)
        {
            _contentService = contentService;
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            var contents = await _contentService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();

            var model = new HomeVM
            {
                Contents = contents.ToList(),
                Abouts = abouts.ToList()
            };

            return View(model);
        }
    }
}
