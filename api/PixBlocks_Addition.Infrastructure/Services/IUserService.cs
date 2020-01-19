using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IUserService
    {
        Task ChangePassword(string login, string newPassword, string oldPassword);
        Task ChangeEmail(string login, string email);
        Task SetPremium(string login);
        Task TakePremium(string login);
    }
}
