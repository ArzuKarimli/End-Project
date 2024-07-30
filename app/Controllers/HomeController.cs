
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
        public HomeController(IContentService contentService)
        {
            _contentService = contentService;
        }
        public async Task<IActionResult> Index()
        {
            List<Content> contents= (List<Content>)await _contentService.GetAllAsync();
            HomeVM model = new()
            {
                Contents = contents

            };
            return View(model);
        }
    }
}
