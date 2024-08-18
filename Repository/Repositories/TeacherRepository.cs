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

namespace Repository.Repositories
{
    public class TeacherRepository:BaseRepository<Teacher>,ITeacherRepository
    {
        public TeacherRepository(AppDbContext dbContext):base(dbContext) { }

        public async Task<Teacher> FindByFullNameAsync(string fullName)
        {
            return await _entities.FirstOrDefaultAsync(t => t.FullName == fullName);
        }

        public async Task<IEnumerable<Teacher>> GetAllTeacherWithCoursesAsync()
        {
            return await _entities.Include(t => t.Courses)
                                   .ThenInclude(c => c.CourseCategory)
                                    .ToListAsync();

        }
    }
}
