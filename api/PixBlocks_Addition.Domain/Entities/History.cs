using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class History
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid CourseId { get; protected set; }

        public virtual Course Course { get; protected set; }
        public virtual User User { get; protected set; }

        protected History() { }

        public History(Course course, User user)
        {
            UserId = user.Id;
            CourseId = course.Id;
        }
    }
}
