using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class UserCourseHistory
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public ICollection<Course> Courses { get; protected set; } = new HashSet<Course>();



        public UserCourseHistory(Guid userId, Course course)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Courses.Add(course);
        }

        protected UserCourseHistory()
        {
        }
    }
}
