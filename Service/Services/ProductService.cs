using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IEnumerable<Product>> GetAllAsyncWithImages()
        {
           return await _productRepo.GetAllWithCategoryAndProductImages();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
           return await _productRepo.GetByIdWithImagesAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string searchText)
        {
           return await _productRepo.SearchProductAsync(searchText);
        }
    }
}
