using System;
using System.Collections.Generic;
using System.Text;
using PixBlocks_Addition.Domain.Entities;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IDBCourseRepository
    {
        Course Get(int id);
        Course Get(string name);
        IEnumerable<Course> GetAll();
        void Add(Course course);
        void Remove(int courseId);
        void Update();

    }
}
