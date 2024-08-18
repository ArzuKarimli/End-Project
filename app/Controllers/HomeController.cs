
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
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IProductService _productService;
        public HomeController(IContentService contentService, IAboutService aboutService, ICourseService courseService, ITeacherService teacherService, IProductService productService)
        {
            _contentService = contentService;
            _aboutService = aboutService;
            _courseService = courseService;
            _teacherService = teacherService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var contents = await _contentService.GetAllAsync();
            var abouts = await _aboutService.GetAllAsync();
            var courses= await _courseService.GetAllWithCategoriesAsync();
            var teachers= await _teacherService.GetAllWithCoursesAsync();
            var products= await _productService.GetAllAsyncWithImages();
            var model = new HomeVM
            {
                Contents = contents.ToList(),
                Abouts = abouts.ToList(),
                Courses = courses.ToList(),
                Teachers = teachers.ToList(),
                Products=products.ToList(),
            };

            return View(model);
        }
    }
}
