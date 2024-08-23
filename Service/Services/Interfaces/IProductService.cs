using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsyncWithImages();
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductAsync(string searchText);
        Task<List<Product>> GetProductsByCategoryIdAsync(int id);
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
        Task UpdateAsync(Product product);      
        Task<List<Product>> GetAllPaginationAsync(int page, int take = 4);
        Task<int> GetCountAsync();
        Task AddReview(Review review);
    }
}
