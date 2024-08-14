using Domain.Entities;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repository.Repositories.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetAllOrderByDescendingAsync();
        Task<bool> ExistAsync(string name);
        Task<ProductCategory> GetWithProductAsync(int id);
        Task<IEnumerable<SelectListItem>> GetAllSelectAsync();
    }
}
