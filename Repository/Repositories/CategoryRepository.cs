using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository:BaseRepository<ProductCategory>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext):base(dbContext) { }

        public async Task<bool> ExistAsync(string name)
        {
            return await _entities.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllOrderByDescendingAsync()
        {
            var categories = await _entities.OrderByDescending(m => m.Id).ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetWithProductAsync(int id)
        {
            return await _entities.Where(m => m.Id == id).Include(m => m.Products).FirstOrDefaultAsync();
        }
    }
}
