using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel.TeacherPage;

namespace app.Controllers
{

    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public TeacherController(ITeacherService teacherService, ICourseCategoryService courseCategoryService,ICourseService courseService, UserManager<AppUser> userManager,IMapper mapper)
        {
            _teacherService = teacherService;
            _courseCategoryService = courseCategoryService;
            _userManager = userManager;
            _courseService = courseService;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var teacher = await _userManager.FindByNameAsync(User.Identity.Name);
            var courses = await _courseService.GetCoursesByTeacherUsernameAsync(teacher.FullName);
   
            return View(_mapper.Map<IEnumerable<TeacherPageVM>>(courses).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var categories = await _courseCategoryService.GetAllAsync();

          
            var model = new CreateCourseVM
            {
                CourseCategories = categories.ToList() 
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CreateCourseVM model)
        {
           

          
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (appUser == null)
            {
              
                return RedirectToAction("Index", "Home");
            }

          
            var teacher = await _teacherService.FindByFullNameAsync(appUser.FullName);

            if (teacher == null)
            {
                
                return RedirectToAction("Index", "Home");
            }

           
            Course newCourse = new()
            {
                Name = model.Name,
                Date = model.Date,
                Duration = model.Duration,
                Price = model.Price,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Seat = model.Seat,
                Level = model.Level,
                Lesson = model.Lesson,
                Image = model.Image,
                Rating = model.Rating,
                Class = model.Class,
                Student = model.StudentCount,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                CourseCategoryId = model.CourseCategoryId,
                Teachers = new List<Teacher> { teacher } 
            };

           
            await _courseService.CreateAsync(newCourse);

            
            return RedirectToAction(nameof(Index));
        }
    }
}
