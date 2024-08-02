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
        public async Task<IEnumerable<CourseCategory>> GetAllAsync()
        {
            return await _courseCategoryRepository.GetAllAsync();
        }
    }
}
