using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Services;

namespace CentralValleyBikes.Services
{
    public class CategoryService : ICategoryService<Category, int>
    {
        private readonly ICategoryRepository<Category, int> _categoryRepository;

        public CategoryService(ICategoryRepository<Category, int> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public Task<(IEnumerable<Category> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public Task<Category> AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
