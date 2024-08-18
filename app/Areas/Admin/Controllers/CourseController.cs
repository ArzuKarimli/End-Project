using app.Areas.Admin.ViewModel.Course;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService,IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllWithCategoriesAsync();
            return View(_mapper.Map<IEnumerable<CourseVM>>(courses).ToList());
        }
    }
}
