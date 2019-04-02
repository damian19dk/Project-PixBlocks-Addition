using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Repositories;
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
            return await (from u in _entities.Users select u).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(string login)
        {
            return await(from u in _entities.Users select u).Where(x => x.Login == login).FirstOrDefaultAsync();
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