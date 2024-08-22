using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Data;
using Service.Services;
using Service.Services.Interfaces;
using Service.ViewModel;
using System.Threading.Tasks;

namespace app.Controllers
{

    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public CartController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var modalJson = _accessor.HttpContext.Request.Cookies["modal"];
                if (modalJson == null)
                    return View(new List<CartVM>());

                List<ModalVM> modalProducts = JsonConvert.DeserializeObject<List<ModalVM>>(modalJson);

                var cartProducts = new List<CartVM>();

                foreach (var modalProduct in modalProducts)
                {
                    var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(m => m.Id == modalProduct.Id);

                    if (product != null)
                    {
                        var image = product.ProductImages.FirstOrDefault()?.Name;

                        cartProducts.Add(new CartVM
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Image = image,
                            Count = modalProduct.TotalCount,
                            Price = product.Price,
                            TotalPrice = modalProduct.TotalCount * product.Price,
                        });
                    }
                }

                decimal totalPrice = cartProducts.Sum(m => m.TotalPrice);
                int productCount = cartProducts.Sum(m => m.Count);

                ViewBag.TotalPrice = totalPrice;
                ViewBag.ProductCount = productCount;

                return View(cartProducts);
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var modalJson = _accessor.HttpContext.Request.Cookies["modal"];
            if (modalJson == null)
                return NotFound();

            List<CartVM> modalproducts = JsonConvert.DeserializeObject<List<CartVM>>(modalJson);
            var product = modalproducts.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                modalproducts.Remove(product);
                _accessor.HttpContext.Response.Cookies.Append("modal", JsonConvert.SerializeObject(modalproducts));
            }

            return Ok();
        }


        [HttpPost]
        public IActionResult IncrementCounterProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            var modalJson = _accessor.HttpContext.Request.Cookies["modal"];
            if (modalJson == null)
                return NotFound();

            List<ModalVM> modal = JsonConvert.DeserializeObject<List<ModalVM>>(modalJson);
            var product = modal.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                product.TotalCount += 1;
                _accessor.HttpContext.Response.Cookies.Append("modal", JsonConvert.SerializeObject(modal));
            }

            decimal totalPrice = modal.Sum(m => m.TotalCount * m.TotalPrice);
            var count = product.TotalCount;
            var price = product.TotalPrice * product.TotalCount;

            return Ok(new { count, totalPrice, price });
        }

        [HttpPost]
        public IActionResult ReductionCounterProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            var modalJson = _accessor.HttpContext.Request.Cookies["modal"];
            if (modalJson == null)
                return NotFound();

            List<ModalVM> modal = JsonConvert.DeserializeObject<List<ModalVM>>(modalJson);
            var product = modal.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                product.TotalCount -= 1;
                if (product.TotalCount <= 0)
                    modal.Remove(product);

                _accessor.HttpContext.Response.Cookies.Append("modal", JsonConvert.SerializeObject(modal));
            }

            decimal totalPrice = modal.Sum(m => m.TotalCount * m.TotalPrice);
            var count = 0;
            var price = 0;
            if (product != null)
            {
                count = product.TotalCount;
                price = (int)(product.TotalPrice * product.TotalCount);
            }
            else
            {
                price = 0;
                count = 0;
            }

            return Ok(new { count, totalPrice, price });
        }
    }
}



