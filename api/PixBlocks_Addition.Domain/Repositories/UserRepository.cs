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
            await _entities.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id) => await _entities.Users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string login) => await _entities.Users.SingleOrDefaultAsync(x => x.Login == login);

        public async Task<IEnumerable<User>> GetAllAsync() => await _entities.Users.ToListAsync();

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _entities.Users.Remove(user);
            await _entities.SaveChangesAsync();
        }

        public async Task RemoveAsync(string login)
        {
            var user = await GetAsync(login);
            _entities.Users.Remove(user);
            await _entities.SaveChangesAsync();
        }

        public async Task<bool> IsEmailUnique(string email)
        {
          var z = await _entities.Users.AnyAsync(x => x.E_mail == email);
            return !z;
        }                     
        public async Task UpdateAsync(User user)
        {
            _entities.Users.Update(user);
            await _entities.SaveChangesAsync();
        }
        public async Task UpdateStatusAsnc(Guid id, int status)
        {
            var user = await GetAsync(id);
            user.SetStatus(status);
            await _entities.SaveChangesAsync();
        }
    }
}