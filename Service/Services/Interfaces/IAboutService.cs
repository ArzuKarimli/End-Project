using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task DeleteAsync(About model);
        Task CreateAsync(About model);
        Task UpdateAsync(About model);
        Task<About> GetByIdAsync(int id);
    }
}
