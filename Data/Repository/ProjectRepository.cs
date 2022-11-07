using Core;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Repository;

namespace MinimalApi.Data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task  AddAsync(Project project)
        {   
            await _context.AddAsync(project);
        }

        public void Remove(Project project)
        {       
            _context.Project.Remove(project);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            
            return await _context.Project.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Project.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Project>> GetProjectsWithCategoryAsync()
        {
            return await _context.Project.Include(x => x.Category).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}