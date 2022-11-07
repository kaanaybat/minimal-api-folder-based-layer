using Core;

namespace MinimalApi.Core.Repository
{
    public interface ICategoryRepository
    {
        Task SaveChangesAsync();
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category model);

        void Delete(Category model);
    }
}