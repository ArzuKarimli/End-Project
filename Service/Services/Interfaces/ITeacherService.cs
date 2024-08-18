using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllWithCoursesAsync();
        Task DeleteAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task CreateAsync(Teacher teacher);
        Task<Teacher> GetByIdAsync(int id);
        Task<Teacher> FindByFullNameAsync(string fullName);
    }
}
