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

        public async Task<User> GetAsync(Guid id) => await _entities.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string login) => await _entities.Users.SingleOrDefaultAsync(x => x.Login == login);

        public async Task<bool> IsEmailUnique(string email) => await _entities.Users.AnyAsync(x => x.E_mail == email);
            
                                               
        public async Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}