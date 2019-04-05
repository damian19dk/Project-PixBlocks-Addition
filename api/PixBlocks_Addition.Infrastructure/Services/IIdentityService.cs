using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface IIdentityService
    {
        Task<JwtDto> Login(string username, string password);
        Task Register(Guid id, string username, string password, string email, int role);
        Task<JwtDto> RefreshAccessToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
    }
}
