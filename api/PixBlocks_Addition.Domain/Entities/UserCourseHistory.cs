using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class UserCourseHistory
    {
        public Guid Id { get; protected set; }
        public User User { get; protected set; }
        public ICollection<Course> Courses { get; protected set; } = new HashSet<Course>();



        public UserCourseHistory(User user, Course course)
        {
            Id = Guid.NewGuid();
            User = user;
            Courses.Add(course);
        }

        protected UserCourseHistory()
        {
        }
    }
}
