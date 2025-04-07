using ProductManagement.Domain.Entities;
using System.Linq.Expressions;

namespace ProductManagement.Application.Interfaces.Repositories
{
    public interface IGenericRepository<E> where E : class 
    {
        Task AddAsync(E entity);
        Task UpdateAsync(E entity);
        Task DeleteAsync(E entity);
        Task<E?> GetByIdAsync(int id);
        Task<IEnumerable<E>> GetAllAsync();
        Task<IEnumerable<E>> FindAsync(Expression<Func<E, bool>> predicate);
    
    }
}
