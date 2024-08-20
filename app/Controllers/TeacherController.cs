using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel.TeacherPage;
using app.Areas.Admin.Helpers.Extentions;
namespace app.Controllers
{

    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ICourseCategoryService _courseCategoryService;
        private readonly ICourseService _courseService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public TeacherController(ITeacherService teacherService, ICourseCategoryService courseCategoryService,ICourseService courseService, UserManager<AppUser> userManager,IMapper mapper,   IWebHostEnvironment env)
        {
            _teacherService = teacherService;
            _courseCategoryService = courseCategoryService;
            _userManager = userManager;
            _courseService = courseService;
            _mapper = mapper;
            _env = env;
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
            if (teacher == null) return RedirectToAction("Index", "Home");

            var courses = await _courseService.GetCoursesByTeacherUsernameAsync(teacher.FullName);
            var model = _mapper.Map<IEnumerable<TeacherPageVM>>(courses).ToList();

            return View(model);
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
            if (appUser == null) return RedirectToAction("Index", "Home");

            var teacher = await _teacherService.FindByFullNameAsync(appUser.FullName);
            if (teacher == null) return RedirectToAction("Index", "Home");

            string fileName = null;
            if (model.Image != null)
            {

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
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
                Image = fileName,
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

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture,TeacherPageVM model)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var teacher = await _userManager.FindByNameAsync(User.Identity.Name);
                var teacherEntity = await _teacherService.FindByFullNameAsync(teacher.FullName);

                string fileName = Guid.NewGuid().ToString() + "-" + profilePicture.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);
                var ProdileImage = model.ProfileImage;

                ProdileImage.SaveFileToLocalAsync(path);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

               
                teacherEntity.Image =fileName;
               

                await _teacherService.UpdateAsync(teacherEntity);
                ViewBag.ProfilePicture = fileName;

                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(Profile));
        }
        [HttpGet]
        public async Task<IActionResult> EditCourse(int? id)
        {
            var course = await _courseService.GetByIdAsync((int)id);
            if (course == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categories = await _courseCategoryService.GetAllAsync();

          

            return View(_mapper.Map<EditCourseVM>(course));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(EditCourseVM model)
        {
            

            var course = await _courseService.GetByIdAsync(model.Id);
            if (course == null)
            {
                return RedirectToAction("Index", "Home");
            }

           
            if (model.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImage.FileName);
                string path = Path.Combine(_env.WebRootPath, "assets/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.NewImage.CopyToAsync(stream);
                }

                course.Image = fileName; 
            }

           
            course.Name = model.Name;
            course.Date = model.Date;
            course.Duration = model.Duration;
            course.Price = model.Price;
            course.Description = model.Description;
            course.FullDescription = model.FullDescription;
            course.Seat = model.Seat;
            course.Level = model.Level;
            course.Lesson = model.Lesson;
            course.Rating = model.Rating;
            course.Class = model.Class;
            course.Student = model.StudentCount;
            course.StartTime = model.StartTime;
            course.EndTime = model.EndTime;
            course.CourseCategoryId = model.CourseCategoryId;

            await _courseService.UpdateAsync(course);

            return RedirectToAction(nameof(Index));
        }

    }
}
