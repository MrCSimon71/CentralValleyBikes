using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Helpers;

namespace CentralValleyBikes.Data
{
    public class OrderRepository : IOrderRepository<Order, int>
    {
        private readonly BikeStoresContext _dbContext;

        public OrderRepository(BikeStoresContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                var orders = await _dbContext.Orders
                    .ToListAsync();

                return orders;
            }
            catch { throw; }
        }

        public async Task<IEnumerable<Order>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            try
            {
                var orders = new List<Order>();

                var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

                var whereClause = ExpressionBuilder.BuildExpression<Order>(filter.SearchCriteria);

                if (whereClause != null)
                {
                    orders = await _dbContext.Orders
                        .Where(whereClause)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    orders = await _dbContext.Orders
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }

                return orders;
            }
            catch { throw; }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            try
            {
                var order = new Order();

                order = await _dbContext.Orders
                    .FirstOrDefaultAsync(p => p.OrderId == id);

                return order;
            }
            catch { throw; }
        }

        public async Task<Order> AddAsync(Order product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.Orders.Add(product);
                
                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return product;
        }

        public async Task<bool> UpdateAsync(Order product)
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

        public async Task<bool> DeleteAsync(Order product)
        {
            try
            {
                _dbContext.Orders.Remove(product);

                await _dbContext.SaveChangesAsync();
            }
            catch { throw; }

            return true;
        }
        
        public async Task<int> GetCountAsync() => await _dbContext.Orders.CountAsync();

        public async Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

            var whereClause = ExpressionBuilder.BuildExpression<Order>(filter.SearchCriteria);

            if (whereClause != null)
            {
                return await _dbContext.Orders
                    .Where(whereClause)
                    .CountAsync();
            }
            else
            {
                return await _dbContext.Orders.CountAsync();
            }
        }
    }
}
