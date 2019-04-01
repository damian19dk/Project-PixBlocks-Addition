using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Category
    {
        public int Id { get; protected set; }
        public int CourseId { get; protected set; }
        public string CategoryName { get; protected set; }

        public static Category Test1 => new Category { Id = 1, CategoryName = "Test1" };
        public static Category Test2 => new Category { Id = 2, CategoryName = "Test2" };

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
