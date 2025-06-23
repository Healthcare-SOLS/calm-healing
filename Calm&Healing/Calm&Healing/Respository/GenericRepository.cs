using Calm_Healing.DAL.Models;
using Calm_Healing.Respository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Calm_Healing.Respository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TenantDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(TenantDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }


}
