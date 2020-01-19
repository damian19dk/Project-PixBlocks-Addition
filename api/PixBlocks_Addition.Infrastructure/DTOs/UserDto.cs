using System;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool Is_premium { get; set; }
        public int Status { get; set; }
    }
}
