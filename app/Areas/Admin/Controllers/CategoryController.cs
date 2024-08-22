using app.Areas.Admin.ViewModel.ProductCategoryVM;

using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllOrderByDescendingAsync();
            return View(categories.Select(m => new CategoryVM { Id = m.Id, Name = m.Name }).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existCategory = await _categoryService.ExistAsync(category.Name);

            if (existCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _categoryService.CreateAsync(new ProductCategory { Name = category.Name });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            ProductCategory category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = category.Name,
                ProductCount = category.Products.Count
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            ProductCategory category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();
            _categoryService.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            ProductCategory category = await _categoryService.GetWithProductAsync((int)id);

            if (category is null) return NotFound();
            return View(new CategoryEditVM { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM category)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();

            ProductCategory existCategory = await _categoryService.GetByIdAsync((int)id);
           existCategory.Name = category.Name;
            await _categoryService.EditAsync(existCategory);
            if (existCategory is null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}

