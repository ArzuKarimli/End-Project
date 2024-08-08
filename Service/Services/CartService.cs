using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;

        public CartService(IHttpContextAccessor accessor, AppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public async Task<List<ModalVM>> AddProductToModalAsync(int id)
        {
            List<ModalVM> modalProducts;

            if (_accessor.HttpContext.Request.Cookies.TryGetValue("modal", out string modalCookieValue))
            {
                modalProducts = JsonConvert.DeserializeObject<List<ModalVM>>(modalCookieValue) ?? new List<ModalVM>();
            }
            else
            {
                modalProducts = new List<ModalVM>();
            }

            var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (dbProduct == null)
            {
                throw new Exception("Product not found");
            }

            var existingProduct = modalProducts.FirstOrDefault(m => m.Id == id);
            if (existingProduct != null)
            {
                existingProduct.TotalCount++;
            }
            else
            {
                modalProducts.Add(new ModalVM
                {
                    Id = id,
                    TotalCount = 1,
                    TotalPrice = dbProduct.Price
                });
            }

            _accessor.HttpContext.Response.Cookies.Append("modal", JsonConvert.SerializeObject(modalProducts));

            return modalProducts;
        }
    }
}



