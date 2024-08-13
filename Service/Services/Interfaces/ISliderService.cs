using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderService
    {
       Task<IEnumerable<Slider>> GetAllAsync();  
        Task<Slider> GetByIdForSliderAsync(int id);      
        Task AddAsync(Slider slider);
        Task DeleteSliderAsync(Slider slider);
        Task<IEnumerable<Slider>> GetAllWithInfoAsync();
        Task<Slider> GetByIdWithInfoAsync(int id);
        Task UpdateAsync(Slider slider);
    }
}
