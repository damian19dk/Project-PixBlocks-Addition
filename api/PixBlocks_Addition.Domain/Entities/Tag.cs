using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Color { get; protected set; }


        public Tag(string name, string description, string color)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetDescription(description);
            SetColor(color);
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name) || name.Length < 4)
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name must have at least 4 characters.");
            }
            if(name.Contains(',') || name.Contains(';'))
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name cannot contain , or ;");
            }
            Name = name;
        }

        public void SetColor(string color)
        {
            if (color == null)
            {
                throw new MyException(MyCodesNumbers.InvalidColor, "Color cannot be null.");
            }
            Color = color;
        }

        public void SetDescription(string description)
        {
            if(description == null)
            {
                throw new MyException(MyCodesNumbers.InvalidDescription, "Description cannot be null.");
            }
            Description = description;
        }

        protected Tag()
        {
        }
    }
}
