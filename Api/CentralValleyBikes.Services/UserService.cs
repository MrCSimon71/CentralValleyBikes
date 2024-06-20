using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Domain.Helpers;
using CentralValleyBikes.Domain.Repositories;
using CentralValleyBikes.Domain.Services;

namespace CentralValleyBikes.Services
{
    public class UserService : IUserService<User, Guid>
    {
        private readonly IUserRepository<User, Guid> _userRepository;

        public UserService(IUserRepository<User, Guid> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<User> Data, int TotalRecords)> GetAllAsync<T>(T searchFilter) where T : SearchFilter
        {
            var users = await _userRepository.GetAllAsync<T>(searchFilter);

            var totalRecords = _userRepository.GetCountAsync<T>(searchFilter).Result;

            return (users, totalRecords);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> AddAsync(User user)
        {
            if (user.Password == null)
            {
                var randonPassword = PasswordHelper.GenerateRandomPassword();
                var randomPasswordHash = PasswordHelper.EncryptPassword(randonPassword);
                user.Password = randomPasswordHash;
            }
            else
            {
                user.Password = PasswordHelper.EncryptPassword(user.Password);
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);

            if (user != null)
            {
                return await _userRepository.DeleteAsync(user);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
