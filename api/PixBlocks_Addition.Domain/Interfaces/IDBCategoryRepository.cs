using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Interfaces
{
    public interface IDBCategoryRepository
    {
        Category Add(Category category);
        Category Get(int id);
        IEnumerable<Category> GetAll();
    }
}
