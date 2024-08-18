using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task CreateAsync(Teacher teacher)
        {
           await _teacherRepository.CreateAsync(teacher);
        }

        public async Task DeleteAsync(Teacher teacher)
        {
          await _teacherRepository.DeleteAsync(teacher);
        }

        public async Task<Teacher> FindByFullNameAsync(string fullName)
        {
           return await _teacherRepository.FindByFullNameAsync(fullName);
        }

        public async Task<IEnumerable<Teacher>> GetAllWithCoursesAsync()
        {
           return await _teacherRepository.GetAllTeacherWithCoursesAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _teacherRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Teacher teacher)
        {
           await _teacherRepository.UpdateAsync(teacher);
        }
    }
}
