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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(ProductCategory category)
        {
            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(ProductCategory category)
        {
           await _categoryRepository.DeleteAsync(category);
        }

        public async Task EditAsync(ProductCategory category)
        {
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> ExistAsync(string name)
        {
          return await _categoryRepository.ExistAsync(name);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
           return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetAllOrderByDescendingAsync()
        {
           return await _categoryRepository.GetAllOrderByDescendingAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id); 
        }

        public async Task<ProductCategory> GetWithProductAsync(int id)
        {
            return await _categoryRepository.GetWithProductAsync(id);
        }
    }
}
