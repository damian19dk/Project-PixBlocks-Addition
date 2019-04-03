using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string login);
        Task<IEnumerable<User>> GetAllAsync();
        Task<bool> IsEmailUnique(string email);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string login);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}