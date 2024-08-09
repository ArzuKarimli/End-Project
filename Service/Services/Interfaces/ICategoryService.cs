using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<IEnumerable<ProductCategory>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(ProductCategory category);
        Task<ProductCategory> GetWithProductAsync(int id);
        Task DeleteAsync(ProductCategory category);
        Task EditAsync(ProductCategory category);
        Task<ProductCategory> GetByIdAsync(int id);
    }
}
