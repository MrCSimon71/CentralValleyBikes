
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Domain.Services
{
    public interface IBaseService<TEntity, TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<(IEnumerable<TEntity> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter;
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TId id);
    }
}   
