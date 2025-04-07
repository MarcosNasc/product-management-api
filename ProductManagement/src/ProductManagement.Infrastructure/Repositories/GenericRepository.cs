using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace ProductManagement.Infrastructure.Repositories
{
    public class GenericRepository<E> : IGenericRepository<E> where E : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<E> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<E>();
        }

        public async Task AddAsync(E entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(E entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(E entity)
        {
             _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<E>> FindAsync(Expression<Func<E, bool>> predicate)
        {
             return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<E>> GetAllAsync()
        {
            return await _dbSet
                .Where(e => EF.Property<bool>(e, "IsActive") == true)
                .ToListAsync();
        }

        public async Task<E?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

       
    }
}
