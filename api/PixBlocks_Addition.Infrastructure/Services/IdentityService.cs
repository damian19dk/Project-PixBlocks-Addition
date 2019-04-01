using PixBlocks_Addition.Domain.Repositories;
using PixBlocks_Addition.Infrastructure.DTOs;
using PixBlocks_Addition.Infrastructure.Models;
using PixBlocks_Addition.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly IJwtHandler _jwtHandler;

        public IdentityService(IJwtHandler jwtHandler,
            IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
            _refreshTokens = refreshTokenRepository;
        }

        public Task Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<JwtDto> Login(string username, string password)
        {
            var user = await _userRepository.GetAsync(username);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.Create(user.UserId, username, "User");
            var refreshToken = Guid.NewGuid().ToString()
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
            var jwtDto = new JwtDto() { AccessToken = jwt.AccessToken, Expires = jwt.Expires, RefreshToken = refreshToken };
            await _refreshTokens.AddAsync(new RefreshToken { Username = username, Token = refreshToken });

            return jwtDto;
        }

        public async Task<JwtDto> RefreshAccessToken(string refreshToken)
        {
            var token = await _refreshTokens.GetAsync(refreshToken);
            if (token == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (token.Revoked)
            {
                throw new Exception("Refresh token was revoked");
            }
            var user = await _userRepository.GetAsync(token.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var jwt = _jwtHandler.Create(user.UserId, user.Username, user.Role);
            var jwtDto = new JwtDto() { AccessToken = jwt.AccessToken, Expires = jwt.Expires, RefreshToken = token.Token };

            return jwtDto;
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            var token = await _refreshTokens.GetAsync(refreshToken);
            if (token == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (token.Revoked)
            {
                throw new Exception("Refresh token was already revoked.");
            }
            token.Revoked = true;
        }
    }
}
