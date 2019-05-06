using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Api.ResourceModels
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string E_mail {get; set;}
        public int RoleId { get; set; }
    }
}
