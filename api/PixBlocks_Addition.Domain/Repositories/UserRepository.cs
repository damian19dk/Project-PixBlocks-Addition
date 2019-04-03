using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PixBlocks_Addition.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PixBlocks_Addition.Domain.Contexts;

namespace PixBlocks_Addition.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PixBlocksContext _context;
        public UserRepository(PixBlocksContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            if (user.UserId == Guid.Empty)
                user.UserId = Guid.NewGuid();
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
           return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetAsync(string login)
        {
           return await _context.Users.SingleOrDefaultAsync(u => u.Username == login);
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
