using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Course course)
        {
          await _courseRepository.CreateAsync(course);
        }

        public async Task DeleteAsync(Course course)
        {
            await _courseRepository.DeleteAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
           return await _courseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithCategoriesAsync()
        {
          return await _courseRepository.GetAllWithCategories();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

      

        public async Task<Course> GetByIdCourseWithCategoryAsync(int id)
        {
            return await _courseRepository.GetByIdCourseWithCategoryAsync(id);
        }

        public async Task<Course> GetByIdWithCategory(int id)
        {
          var course= _courseRepository.FindBy(m=>m.Id == id,m=>m.CourseCategory,m=>m.Teachers).FirstOrDefault();
            return course;
        }

        public async Task<IEnumerable<Course>> GetCoursesByTeacherUsernameAsync(string fullname)
        {
          return await _courseRepository.GetCoursesByTeacherUsernameAsync(fullname);
        }

        public async Task UpdateAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }
    }
}
