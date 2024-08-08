using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.TeacherPage;

namespace app.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ICourseCategoryService _courseCategoryService;
        public TeacherController(ITeacherService teacherService, ICourseCategoryService courseCategoryService)
        {
            _teacherService = teacherService;
            _courseCategoryService = courseCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var datas= await _teacherService.GetAllWithCoursesAsync();
            var categories= await _courseCategoryService.GetAllAsync();
            TeacherVM model = new()
            {
               Teachers=datas.ToList(),
               CourseCategories=categories.ToList()
            };
            return View(model);
        }
    }
}
