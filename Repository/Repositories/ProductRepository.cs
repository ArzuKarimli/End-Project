using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extentions;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Repositories
{
    public class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext dbContext):base(dbContext) { }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAndProductImages()
        {
            return await _entities.IncludeMultiple(
                 m => m.ProductImages,
                 m => m.Category
             ).ToListAsync();
        }

        public async Task<Product> GetByIdWithImagesAsync(int id)
        {
            return await _entities
                .IncludeMultiple(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(string searchText)
        {
            return await _entities.Include(p => p.Category)
                                                      .Where(p => p.Name.Contains(searchText) || p.Category.Name.Contains(searchText))
                                                      .ToListAsync();

        }

    }
}
