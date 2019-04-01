using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IDBUserRepository
    {
        User Get(int id);
        User Get(string login);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Remove(int id);
        void Update();
    }
}
