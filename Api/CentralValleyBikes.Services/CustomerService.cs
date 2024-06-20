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
    public class CustomerService : ICustomerService<Customer, int>
    {
        private readonly ICustomerRepository<Customer, int> _customerRepository;

        public CustomerService(ICustomerRepository<Customer, int> customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<Customer> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var customers = await _customerRepository.GetAllAsync<T>(searchFilter);

            //var totalRecords = customers.Count();
            var totalRecords = _customerRepository.GetCountAsync<T>(searchFilter).Result;

            return (customers, totalRecords);
        }

        public Task<Customer> GetByIdAsync(int id)
        {
            return _customerRepository.GetByIdAsync(id);
        }

        public Task<Customer> AddAsync(Customer customer)
        {
            return _customerRepository.AddAsync(customer);
        }

        public Task<bool> UpdateAsync(Customer customer)
        {
            return _customerRepository.UpdateAsync(customer);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var customer = GetByIdAsync(id);

            if (customer != null)
            {
                return _customerRepository.DeleteAsync(customer.Result);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
