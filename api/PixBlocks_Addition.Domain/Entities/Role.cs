using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public virtual ICollection<User> Users { get; set; }

        public static Role Student => new Role { Id = 1, Name = "Użytkownik" };
        public static Role Administrator = new Role { Id = 2, Name = "Administrator" };

        public static string GetRoleName(int id)
        {
            switch (id)
            {
                case 1: return "Użytkownik";
                case 2: return "Administrator";
                default: return "";
            }
        }

    }
}
