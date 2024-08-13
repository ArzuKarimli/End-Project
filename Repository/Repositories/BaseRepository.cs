using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbcontext;
        protected readonly DbSet<T> _entities;
       
        public BaseRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _entities = _dbcontext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _dbcontext.SaveChangesAsync();
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _entities.Where(predicate);
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public IQueryable<T> FindAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = _entities.AsQueryable();
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


    }
}
