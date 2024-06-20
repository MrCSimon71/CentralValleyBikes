using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.Entities;

namespace CentralValleyBikes.Domain.Repositories
{
    public interface IOrderRepository<Order, TId> : IBaseRepository<Order, TId>
    {
    }
}
