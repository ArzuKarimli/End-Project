using app.Areas.Admin.Helpers.Extentions;
using app.Areas.Admin.ViewModel.Course;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CourseController(ICourseService courseService,IMapper mapper, IWebHostEnvironment env)
        {
            _courseService = courseService;
            _mapper = mapper;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllWithCategoriesAsync();
            return View(_mapper.Map<IEnumerable<CourseVM>>(courses).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var course = await _courseService.GetByIdAsync((int)id);
            if(course is null) return NotFound();
            return View(_mapper.Map<CourseDetailVM>(course));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Course course = await _courseService.GetByIdAsync((int)id);
            if (course == null) return NotFound();

           
             string path = Path.Combine(_env.WebRootPath, "images", course.Name);
             path.DeleteFileFromLocal();
            

            await _courseService.DeleteAsync(course);
            return RedirectToAction(nameof(Index));
        }
    }
}
