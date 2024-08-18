using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> GetAllWithCategoriesAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithCategory(int id);
        Task DeleteAsync(Course course);
        Task UpdateAsync(Course course);
        Task CreateAsync(Course course);
        Task<IEnumerable<Course>> GetCoursesByTeacherUsernameAsync(string fullname);


    }
}
