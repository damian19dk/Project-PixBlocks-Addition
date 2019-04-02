using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly PixBlocksContext _entities;
        public UserRepository(PixBlocksContext entities)
        {
            _entities = entities;
        }

        public async Task AddAsync(User user)
        {
            await _entities.Users.AddAsync(user);
        }

        public async Task<User> GetAsync(Guid id)
        {
            await _entities.
        }

        public Task<User> GetAsync(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailUnique(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}