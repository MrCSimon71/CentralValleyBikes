using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Helpers;

namespace CentralValleyBikes.Data
{
    public class ProductRepository : IProductRepository<Product, int>
    {
        private readonly BikeStoresContext _dbContext;

        public ProductRepository(BikeStoresContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                var products = await _dbContext.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .ToListAsync();

                return products;
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Product>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            try
            {
                var products = new List<Product>();

                var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

                var whereClause = ExpressionBuilder.BuildExpression<Product>(filter.SearchCriteria);

                if (whereClause != null)
                {
                    products = await _dbContext.Products
                        .Where(whereClause)
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    products = await _dbContext.Products
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }

                return products;
            }
            catch { throw; }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                var product = new Product();

                product = await _dbContext.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.ProductId == id);

                return product;
            }
            catch { throw; }
        }

        public async Task<Product> AddAsync(Product product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.Products.Add(product);
                
                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.Update(product);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            try
            {
                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }
        
        public async Task<int> GetCountAsync() => await _dbContext.Products.CountAsync();

        public async Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

            var whereClause = ExpressionBuilder.BuildExpression<Product>(filter.SearchCriteria);

            if (whereClause != null)
            {
                return await _dbContext.Products
                    .Where(whereClause)
                    .CountAsync();
            }
            else
            {
                return await _dbContext.Products.CountAsync();
            }
        }
    }
}
