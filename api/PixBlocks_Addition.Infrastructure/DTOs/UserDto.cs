using System;

namespace PixBlocks_Addition.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string E_mail { get; set; }
        public int RoleId { get; set; }
        public bool Is_premium { get; set; }
        public int Status { get; set; }
    }
}
