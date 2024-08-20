using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public TeacherRepository(AppDbContext dbContext, UserManager<AppUser> userManager) :base(dbContext) 
         {
            _userManager = userManager;
        }
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
        public async Task<IEnumerable<Teacher>> GetTeachersWithRolesAsync()
        {
            
            var allTeachers = await _entities.ToListAsync();
            var teachersWithRoles = new List<Teacher>();

            foreach (var teacher in allTeachers)
            {
                var user = await _userManager.FindByNameAsync(teacher.FullName);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Teacher"))
                {
                    teachersWithRoles.Add(teacher);
                }
            }

            return teachersWithRoles;
        }
    }
}
