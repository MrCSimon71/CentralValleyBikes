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
    public class BrandService : IBrandService<Brand, int>
    {
        private readonly IBrandRepository<Brand, int> _brandRepository;

        public BrandService(IBrandRepository<Brand, int> brandRepository)
        {
            this._brandRepository = brandRepository;
        }

        public Task<IEnumerable<Brand>> GetAllAsync()
        {
            return _brandRepository.GetAllAsync();
        }

        public Task<(IEnumerable<Brand> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public Task<Brand> GetByIdAsync(int id)
        {
            return _brandRepository.GetByIdAsync(id);
        }

        public Task<Brand> AddAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
