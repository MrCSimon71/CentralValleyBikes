
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Domain.Repositories
{
    public interface IBaseRepository<TEntity, TId>
    {
        Task<int> GetCountAsync();
        Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter;
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync<T>(T searchFilter) where T : SearchFilter;
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}
