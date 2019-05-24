using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Category
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public static Category Test1 => new Category { Id = 1, Name = "Test1" };
        public static Category Test2 => new Category { Id = 2, Name = "Test2" };

        public static string GetCategoryName(int id)
        {
            switch (id)
            {
                case 1: return "Test1";
                case 2: return "Test2";
                default: return "";
            }
        }
    }
}
