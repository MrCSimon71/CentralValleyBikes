using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Data
{
    public class CategoryRepository : ICategoryRepository<Category, int>
    {
        private readonly BikeStoresContext _dbContext;

        public CategoryRepository(BikeStoresContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                var categories = await _dbContext.Categories.ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<Category>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            try
            {
                var categories = new Category();

                categories = await _dbContext.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);

                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> AddAsync(Category categories)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Category categories)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Category categories)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync() => await _dbContext.Categories.CountAsync();
        
        public Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }
    }
}
