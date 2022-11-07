using Core;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core.Repository;

namespace MinimalApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task  AddAsync(Category model)
        {
            if(model == null)
               throw new ArgumentNullException(nameof(model));
            
            await _context.AddAsync(model);
        }

        public void Delete(Category model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));
            
            _context.Category.Remove(model);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}