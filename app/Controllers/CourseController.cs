using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.CoursePage;

namespace app.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
       
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var courses= await _courseService.GetAllWithCategoriesAsync();
            CourseVM model = new()
            {
                Courses = courses.ToList(),
            };
            return View(model);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Course course = await _courseService.GetByIdWithCategory((int)id);
            if (course == null) return NotFound();
            var courses= await _courseService.GetAllWithCategoriesAsync();
           
            CourseDetailVM model = new()
            {
                Course = course,
                Courses= courses.ToList(),
            };
            return View(model);
        }
    }
}
