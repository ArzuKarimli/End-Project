﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllTeacherWithCoursesAsync();
         Task<Teacher> FindByFullNameAsync(string fullName);
    }
}
