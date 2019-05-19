using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Course: Media
    {
        public ICollection<CourseVideo> CourseVideos { get; protected set; } = new HashSet<CourseVideo>();
        public ICollection<Lesson> Lessons { get; protected set; } = new HashSet<Lesson>();

        public Course(bool premium, string title, string description, string picture,
            string lang, long duration=0, IEnumerable<Tag> tags = null) 
            :base(string.Empty, premium, title, description, picture, duration, lang, tags)
        {
            if (name.Length < 3) throw new MyException(MyCodes.TooShortCourseName);
            Name = name;
        }
        
        public void SetDescription(string description)
        {
            if (description.Length > 10000) throw new MyException(MyCodes.TooLongDescription);
            Description = description;
        }

        public void SetPicture(string picture)
        {
        }
    }
}
