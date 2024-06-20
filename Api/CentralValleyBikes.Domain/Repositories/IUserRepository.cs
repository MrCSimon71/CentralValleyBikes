using CentralValleyBikes.Domain.Entities;

namespace CentralValleyBikes.Domain.Repositories
{
    public interface IUserRepository<User, TId> : IBaseRepository<User, TId>
    {
        Task<User> GetByEmailAddressAsync(string emailAddress);
    }
}
