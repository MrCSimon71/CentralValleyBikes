
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.JWT;

namespace CentralValleyBikes.Domain.Services
{
    public interface IAuthenticationService<User, TId> : IBaseService<User, TId>
    {
        Task<JWToken> Authenticate(string emailAddress, string password);
    }
}
