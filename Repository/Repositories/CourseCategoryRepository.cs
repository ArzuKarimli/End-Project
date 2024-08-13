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
    public class CourseCategoryRepository:BaseRepository<CourseCategory>,ICourseCategoryRepository
    {
        public CourseCategoryRepository(AppDbContext dbContext): base(dbContext) { }

        public async Task<bool> ExistAsync(string name)
        {
            return await _entities.AnyAsync(m => m.Name == name);
        }
    }
}
