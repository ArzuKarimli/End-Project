using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Helpers.Extentions;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseRepository:BaseRepository<Course>,ICourseRepository
    {
        public CourseRepository(AppDbContext dbContext): base(dbContext) { }

        public async Task<IEnumerable<Course>> GetAllWithCategories()
        {
            return await _entities.IncludeMultiple<Course>(m => m.CourseCategory).ToListAsync();
        }

       
    }
}
