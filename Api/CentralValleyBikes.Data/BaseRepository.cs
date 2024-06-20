using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CentralValleyBikes.Data
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        protected BikeStoresContext DBContext { get; set; }

        public BaseRepository(BikeStoresContext dbContext)
        {
            this.DBContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await this.DBContext.Set<TEntity>().AsNoTracking().ToListAsync<TEntity>();
            }
            catch { throw; }
        }

        public Task<IEnumerable<TEntity>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            try
            {
                return await this.DBContext.Set<TEntity>().FindAsync(id);
            }
            catch { throw; }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                this.DBContext.Set<TEntity>().Add(entity);
                await this.DBContext.SaveChangesAsync();
            }
            catch { throw; }

            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                this.DBContext.Set<TEntity>().Update(entity);
                await this.DBContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                this.DBContext.Set<TEntity>().Remove(entity);
                await this.DBContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<int> GetCountAsync() => await this.DBContext.Set<TEntity>().CountAsync();

        public Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }
    }
}
