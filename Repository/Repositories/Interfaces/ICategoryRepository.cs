﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task<ProductCategory> GetWithProductAsync(int id);
    }
}
