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
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public async Task CreateAsync(CourseCategory category)
        {
            await _courseCategoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(CourseCategory category)
        {
            await _courseCategoryRepository.DeleteAsync(category);
        }

        public async Task EditAsync(CourseCategory category)
        {
           await _courseCategoryRepository.UpdateAsync(category);
        }

        public async Task<bool> ExistAsync(string name)
        {
          return  await _courseCategoryRepository.ExistAsync(name);
        }

        public async Task<IEnumerable<CourseCategory>> GetAllAsync()
        {
            return await _courseCategoryRepository.GetAllAsync();
        }

        public async Task<CourseCategory> GetByIdAsync(int id)
        {
           return await _courseCategoryRepository.GetByIdAsync(id);
        }
    }
}
