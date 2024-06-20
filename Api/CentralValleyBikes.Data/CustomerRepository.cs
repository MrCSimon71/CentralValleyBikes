using Microsoft.EntityFrameworkCore;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Helpers;

namespace CentralValleyBikes.Data
{
    public class CustomerRepository : ICustomerRepository<Customer, int>
    {
        private readonly BikeStoresContext _dbContext;

        public CustomerRepository(BikeStoresContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            try
            {
                var customers = new List<Customer>();

                var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

                var whereClause = ExpressionBuilder.BuildExpression<Customer>(filter.SearchCriteria);

                if (whereClause != null)
                {
                    customers = await _dbContext.Customers
                        .Where(whereClause)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    customers = await _dbContext.Customers
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }

                return customers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                var customer = new Customer();

                customer = await _dbContext.Customers.FirstOrDefaultAsync(p => p.CustomerId == id);

                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                _dbContext.Add(customer);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            
            return customer;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                _dbContext.Update(customer);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(Customer customer)
        {
            try
            {
                _dbContext.Customers.Remove(customer);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        
        public async Task<int> GetCountAsync() => await _dbContext.Customers.CountAsync();

        public async Task<int> GetCountAsync<T>(T searchFilter) where T : SearchFilter
        {
            var filter = (SearchFilter)Convert.ChangeType(searchFilter, typeof(SearchFilter));

            var whereClause = ExpressionBuilder.BuildExpression<Customer>(filter.SearchCriteria);

            if (whereClause != null)
            {
                return await _dbContext.Customers
                    .Where(whereClause)
                    .CountAsync();
            }
            else
            {
                return await _dbContext.Customers.CountAsync();
            }
        }
    }
}
