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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
           return await _courseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithCategories()
        {
          return await _courseRepository.GetAllWithCategories();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<Course> GetByIdWithCategory(int id)
        {
          var course= _courseRepository.FindBy(m=>m.Id == id,m=>m.CourseCategory).FirstOrDefault();
            return course;
        }
    }
}
