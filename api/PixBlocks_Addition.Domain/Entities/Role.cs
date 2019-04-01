using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public virtual ICollection<User> Users { get; set; }

        public static Role Student => new Role { Id = 1, Name = "Student" };
        public static Role Administrator = new Role { Id = 2, Name = "Administrator" };
        public static Role Teacher = new Role { Id = 3, Name = "Teacher" };
        public static Role Moderator = new Role { Id = 4, Name = "Moderator" };

        public static string GetRoleName(int id)
        {
            switch (id)
            {
                case 1: return "Student";
                case 2: return "Administrator";
                case 3: return "Teacher";
                case 4: return "Moderator";
                default: return "";
            }
        }

    }
}
