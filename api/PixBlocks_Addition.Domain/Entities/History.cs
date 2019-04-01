using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class History
    {
        public int Id { get; protected set; }
        public int UserId { get; protected set; }
        public int CourseId { get; protected set; }

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
