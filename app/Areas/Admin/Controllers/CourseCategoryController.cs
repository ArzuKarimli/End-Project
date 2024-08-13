

using app.Areas.Admin.ViewModel.CourseCategory;
using app.Areas.Admin.ViewModel.ProductCategoryVM;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseCategoryController : Controller
    {
        private readonly ICourseCategoryService _courseCategoryService;
        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            _courseCategoryService = courseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories= await _courseCategoryService.GetAllAsync();
            return View(categories.Select(m => new CourseCategoryVM { Id = m.Id, Name = m.Name }).ToList());
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(CategoryCreateVM category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existCategory = await _courseCategoryService.ExistAsync(category.Name);

            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _courseCategoryService.CreateAsync(new CourseCategory { Name = category.Name });
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CourseCategory category = await _courseCategoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            CourseCategoryDetailVM model = new()
            {
                Name = category.Name,
               
            };
            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            CourseCategory category = await _courseCategoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();
            _courseCategoryService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            CourseCategory category = await _courseCategoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();
            return View(new CourseCategoryEditVM { Id = category.Id, Name = category.Name });
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int? id, CourseCategoryEditVM category)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();

            CourseCategory existCategory = await _courseCategoryService.GetByIdAsync((int)id);
            bool existCategoryName = await _courseCategoryService.ExistAsync(existCategory.Name);

            if (existCategoryName)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _courseCategoryService.EditAsync(existCategory);
            if (existCategory is null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
