using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Services;

namespace CentralValleyBikes.Services
{
    public abstract class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : class
    {
        protected IBaseRepository<TEntity, TId> repository { get; set; }

        public BaseService(IBaseRepository<TEntity, TId> repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public Task<(IEnumerable<TEntity> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
            //var data = await repository.Get(paginationFilter);
            //var totalRecords = repository.GetCount().Result;
            //return (data, totalRecords);
        }

        public Task<TEntity> GetByIdAsync(TId id)
        {
            return repository.GetByIdAsync(id);
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            return repository.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            return repository.UpdateAsync(entity);
        }

        public Task<bool> DeleteAsync(TId id)
        {
            Task<TEntity> entity = GetByIdAsync(id);

            return repository.DeleteAsync(entity.Result);
        }
    }
}
