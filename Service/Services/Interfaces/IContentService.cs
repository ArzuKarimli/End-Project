

using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IContentService
    {
        Task<IEnumerable<Content>> GetAllAsync();
        Task DeleteAsync(Content content);
        Task UpdateAsync(Content content);
        Task CreateAsync(Content content);
        Task<Content> GetByIdAsync(int id);

    }
}
