using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        protected Tag()
        {
        }
    }
}
