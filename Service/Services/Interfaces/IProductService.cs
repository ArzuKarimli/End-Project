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
       
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> SearchProductAsync(string searchText);
      
    }
}
