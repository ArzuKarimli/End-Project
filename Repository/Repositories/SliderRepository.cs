using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Repository.Repositories
{
    public class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {

      
        public SliderRepository(AppDbContext dbcontext) : base(dbcontext) { }

        public async Task<IEnumerable<Slider>> GetAllWithInfoAsync()
        {
            return await _entities.Include(m => m.SliderInfo).ToListAsync();
        }

        public async Task<Slider> GetByIdWithInfoAsync(int id)
        {
            return await _entities.Include(m => m.SliderInfo).Where(m => m.Id == id).FirstOrDefaultAsync();
        }
   
    }
}
