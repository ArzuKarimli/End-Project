
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
            List<Content> contents= (List<Content>)await _contentService.GetAllAsync();
            List<About> abouts= (List<About>)await _aboutService.GetAllAsync();
            HomeVM model = new()
            {
                Contents = contents,
                Abouts = abouts

            };
            return View(model);
        }
    }
}
