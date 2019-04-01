using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
