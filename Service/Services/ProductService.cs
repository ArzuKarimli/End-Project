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

        public async Task CreateAsync(Product product)
        {
            await _productRepo.CreateAsync(product);
        }

        public async Task DeleteAsync(Product product)
        {
           await  _productRepo.DeleteAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
          return await _productRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsyncWithImages()
        {
           return await _productRepo.GetAllWithCategoryAndProductImages();
        }

        public async Task<List<Product>> GetAllPaginationAsync(int page, int take = 4)
        {
            return await _productRepo.GetAllPaginationAsync(page, take);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
           return await _productRepo.GetByIdWithImagesAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
          return await _productRepo.GetCountAsync();
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int id)
        {
           return await _productRepo.GetProductsByCategoryIdAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string searchText)
        {
           return await _productRepo.SearchProductAsync(searchText);
        }

        public async Task UpdateAsync(Product product)
        {
           await _productRepo.UpdateAsync(product);
        }
    }
}
