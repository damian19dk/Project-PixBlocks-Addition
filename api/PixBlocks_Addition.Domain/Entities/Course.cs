using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Course: Media
    {
        public ICollection<CourseVideo> CourseVideos { get; protected set; } = new HashSet<CourseVideo>();

        public Course(bool premium, string title, string description, string picture,
            string lang, long duration=0, IEnumerable<string> resources = null, IEnumerable<Tag> tags = null) 
            :base(string.Empty, premium, title, description, picture, duration, lang, resources, tags)
        {
        }
        protected Course()
        {

        }
    }
}
