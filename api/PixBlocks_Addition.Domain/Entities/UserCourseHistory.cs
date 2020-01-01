using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class UserCourseHistory
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Course Course { get; protected set; }



        public UserCourseHistory(Guid userId, Course course)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Course = course;
        }

        protected UserCourseHistory()
        {
        }
    }
}
