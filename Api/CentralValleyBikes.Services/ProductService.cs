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
    public class ProductService : IProductService<Product, int>
    {
        private readonly IProductRepository<Product, int> _productRepository;

        public ProductService(IProductRepository<Product, int> productRepository)
        {
            this._productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<Product> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var products = await _productRepository.GetAllAsync<T>(searchFilter);

            var totalRecords = _productRepository.GetCountAsync<T>(searchFilter).Result;

            return (products, totalRecords);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _productRepository.GetByIdAsync(id);
        }

        public Task<Product> AddAsync(Product product)
        {
            return _productRepository.AddAsync(product);
        }

        public Task<bool> UpdateAsync(Product product)
        {
            return _productRepository.UpdateAsync(product);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = GetByIdAsync(id);

            if (product != null)
            {
                return _productRepository.DeleteAsync(product.Result);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
