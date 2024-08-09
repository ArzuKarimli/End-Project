using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel;

namespace app.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICourseService _courseService;
        public CheckoutController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(int? courseId)
        {
            if (courseId == null) return BadRequest();

            var course = await _courseService.GetByIdWithCategory((int)courseId);
            if (course == null) return NotFound();

            var model = new CheckoutVM
            {
                Course = course,
                TotalPrice = course.Price
            };

            return View(model);
        }
    }
}
