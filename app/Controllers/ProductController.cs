using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel;
using Service.ViewModel.ProductPage;

namespace app.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICartService _cartService;
        public ProductController(IProductService productService, ICategoryService categoryService, ICartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 4)
        {
            var products = await _productService.GetAllPaginationAsync(page, take);
            var totalProducts = await _productService.GetCountAsync();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)take);

            List<ProductCategory> categories = (List<ProductCategory>)await _categoryService.GetAllAsync();

            var model = new ProductVM
            {
                Products = products.ToList(),
                ProductCategories = categories,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }



        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
           
            var products = await _productService.GetAllAsyncWithImages();
            List<ProductCategory> categories = (List<ProductCategory>)await _categoryService.GetAllAsync();

            ProductDetailVM model = new()
            {
                Product = product,
                Products = products.ToList(),
               ProductCategories= categories
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Filter(int? categoryId, int? price, string sort)
        {
            var products = await _productService.GetAllAsyncWithImages();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.Category.Id == categoryId.Value);
            }
         

            if (price.HasValue && price.Value > 0)
            {
                products = products.Where(p => p.Price <= price.Value);
            }

            switch (sort)
            {
                case "price-low-high":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price-high-low":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "az":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "za":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }

            var categories = await _categoryService.GetAllAsync();
             ProductVM model=new()
            {
                Products = products.ToList(),
                ProductCategories = categories.ToList(),
            };

            return PartialView("_ProductList", model);
        }




        [HttpPost]
        
        public async Task<IActionResult> AddProductToModal(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null) return BadRequest();

                List<ModalVM> result = await _cartService.AddProductToModalAsync((int)id);
                int count = result.Sum(m => m.TotalCount);
                decimal total = result.Sum(m => m.TotalCount * m.TotalPrice);

                return Ok(new { count, total });
            }
            else
            {
                return RedirectToAction("SignIn","Account");
            }

                    
        }
        [HttpGet]
        public async Task<IActionResult> Search(string request)
        {
            var products = await _productService.SearchProductAsync(request);
            if (products == null)
            {
                products = new List<Product>();
            }
           
            //List<ProductCategory> categories = (List<ProductCategory>)await _categoryService.GetAllAsync();
            ProductVM model = new()
            {
                Products = products.ToList(),  
                //ProductCategories= categories.ToList(),
            };
            return PartialView("_ProductList", model);
        }
   



    }
}
