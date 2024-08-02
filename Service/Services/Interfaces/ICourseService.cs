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
        Task<IEnumerable<Course>> GetAllWithCategories();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithCategory(int id);
    }
}
