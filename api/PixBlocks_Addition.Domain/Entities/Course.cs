using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Picture { get; protected set; }

        public Course() { }

        public Course(string name, string description, string picture)
        {
            SetName(name);
            SetDescription(description);
            SetPicture(picture);
        }

        public void SetName(string name)
        {
            if (name.Length < 3) throw new Exception();
            Name = name;
        }
        
        public void SetDescription(string description)
        {
            if (description.Length > 10000) throw new Exception();
            Description = description;
        }

        public void SetPicture(string picture)
        {
            Picture = picture;
        }
    }
}
