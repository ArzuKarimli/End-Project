using app.Areas.Admin.Helpers.Extentions;
using app.Areas.Admin.ViewModel.Product;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using Service.ViewModel.ProductPage;
using static System.Net.Mime.MediaTypeNames;
using ProductDetailVM = app.Areas.Admin.ViewModel.Product.ProductDetailVM;
using ProductVM = app.Areas.Admin.ViewModel.Product.ProductVM;

namespace app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsyncWithImages();

            List<ProductVM> model = new List<ProductVM>();
            foreach (var item in products)
            {
                var image = item.ProductImages.FirstOrDefault()?.Name;
                ProductVM result = new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    SalesCount = item.SalesCount,
                    Weight = item.Weight,
                    Category = item.Category.Name,
                    Material = item.Material,
                    Rating = item.Rating,
                    Image = image
                };
                model.Add(result);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            List<ProductImageVM> images = new List<ProductImageVM>();
            foreach (var item in product.ProductImages)
            {
                images.Add(new ProductImageVM
                {
                    Image = item.Name,
                   
                });
            }

            ProductDetailVM model = new()
            {

                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SalesCount = product.SalesCount,
                Weight = product.Weight,
                Category = product.Category.Name,
                Material = product.Material,
                Rating = product.Rating,
                Images = images

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();

            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "images", item.Name);
                path.DeleteFileFromLocal();
            }

            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
