using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Data
{
    public class BrandRepository : IBrandRepository<Brand, int>
    {
        private readonly BikeStoresContext _dbContext;

        public BrandRepository(BikeStoresContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            try
            {
                var brands = await _dbContext.Brands.ToListAsync();
                return brands;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<Brand>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            try
            {
                var brand = new Brand();

                brand = await _dbContext.Brands.FirstOrDefaultAsync(p => p.BrandId == id);

                return brand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync() => await _dbContext.Brands.CountAsync();
        
        public Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }
    }
}
