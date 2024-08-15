using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel.AboutPage;

namespace app.Controllers
{
   
    public class AboutController : Controller
    {
       private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService )
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();

            AboutVM model = new()
            {
               Abouts=abouts.ToList(),

            };
            return View(model);
        }
    }
}
