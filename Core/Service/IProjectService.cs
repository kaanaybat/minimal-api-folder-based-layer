using Core.Dtos;
using Core;

namespace MinimalApi.Core.Service
{
    public interface IProjectService
    {
        Task SaveChangesAsync();
        Task<Project> GetByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task AddAsync(Project model);
        void Remove(Project model);
        Task<List<ProjectWithCategoryDto>> GetProjectsWithCategoryAsync();
    }
}