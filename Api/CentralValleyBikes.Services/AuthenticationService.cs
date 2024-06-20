using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.JWT;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Helpers;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Services;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Services
{
    public class AuthenticationService : IAuthenticationService<User, Guid>
    {
        private readonly IUserRepository<User, Guid> _userRepository;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUserRepository<User, Guid> userRepository, IOptions<AppSettings> appSettings)
        {
            this._userRepository = userRepository;
            this._appSettings = appSettings.Value;
        }

        public Task<JWToken> Authenticate(string emailAddress, string password)
        {
            var user = _userRepository.GetByEmailAddressAsync(emailAddress).Result;

            if (user == null || user.Password != password)
                return Task.FromResult<JWToken>(null);

            var token = GenerateJWToken(user);

            return Task.FromResult(new JWToken()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            });
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<User> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        private string GenerateJWToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
