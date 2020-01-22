using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Course: Media
    {
        public ICollection<CourseVideo> CourseVideos { get; protected set; } = new HashSet<CourseVideo>();
        public Course(bool premium, Guid parentId, string title, string description, string picture,
            string lang, long duration=0, IEnumerable<string> resources = null, IEnumerable<Tag> tags = null) 
            :base(string.Empty, parentId, premium, title, description, picture, duration, lang, resources, tags)
        {
        }
        protected Course()
        {

        }
        public void AddTimeToDuration(long time)
        {
            if (time < 0) throw new MyException(MyCodesNumbers.NegativeTime, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.InvalidTime);
            Duration += time;
        }
        public void TakeTimeFromDuration(long time)
        {
            if (time < 0) throw new MyException(MyCodesNumbers.NegativeTime, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.InvalidTime);
            if (Duration - time < 0) throw new MyException(MyCodesNumbers.InvalidTime, Domain.Exceptions.ExceptionMessages.ServicesExceptionMessages.InvalidTime)
            Duration -= time;
        }
    }
}
