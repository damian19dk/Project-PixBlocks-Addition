using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> GetAsync(Guid id);
        Task<Course> GetAsync(string name);
        Task<IEnumerable<Course>> GetAllAsync();
        Task AddAsync(Course course);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(string name);
        Task UpdateAsync(Course course);

    }
}
