using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICourseCategoryService
    {
        Task<IEnumerable<CourseCategory>> GetAllAsync();
        Task DeleteAsync(CourseCategory category);
        Task EditAsync(CourseCategory category);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(CourseCategory category);
        Task<CourseCategory> GetByIdAsync(int id);
    }
}
