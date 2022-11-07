using Core;

namespace MinimalApi.Core.Repository
{
    public interface IProjectRepository
    {
        Task SaveChangesAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project model);
        void Remove(Project model);
        Task<List<Project>> GetProjectsWithCategoryAsync();
    }
}