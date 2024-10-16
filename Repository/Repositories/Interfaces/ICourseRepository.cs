﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ICourseRepository:IBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllWithCategories();
        Task<IEnumerable<Course>> GetCoursesByTeacherUsernameAsync(string fullname);
        Task<Course> GetByIdCourseWithCategoryAsync(int id);

    }
}
