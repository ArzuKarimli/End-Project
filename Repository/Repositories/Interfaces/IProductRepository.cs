using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAndProductImages();
        Task<Product> GetByIdWithImagesAsync(int id);
        Task<IEnumerable<Product>> SearchProductAsync(string searchText);
    }
}
