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
    public class OrderService : IOrderService<Order, int>
    {
        private readonly IOrderRepository<Order, int> _orderRepository;

        public OrderService(IOrderRepository<Order, int> orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<Order> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var orders = await _orderRepository.GetAllAsync<T>(searchFilter);

            var totalRecords = _orderRepository.GetCountAsync<T>(searchFilter).Result;

            return (orders, totalRecords);
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }

        public Task<Order> AddAsync(Order order)
        {
            return _orderRepository.AddAsync(order);
        }

        public Task<bool> UpdateAsync(Order order)
        {
            return _orderRepository.UpdateAsync(order);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var order = GetByIdAsync(id);

            if (order != null)
            {
                return _orderRepository.DeleteAsync(order.Result);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
