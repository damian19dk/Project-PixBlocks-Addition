using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PixBlocks_Addition.Infrastructure.Extentions;
using PixBlocks_Addition.Infrastructure.Models;
using PixBlocks_Addition.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> jwtSettings)
        {
            _jwtOptions = jwtSettings;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOptions.Value.Issuer,
                ValidateLifetime = _jwtOptions.Value.ValidateLifetime
            };
        }

        public JsonWebToken Create(Guid userId, string login, string role,
            IDictionary<string, string> claims = null)
        {
            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    jwtClaims.Add(new Claim(claim.Key, claim.Value));
                }
            }

            var expires = now.AddMinutes(_jwtOptions.Value.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}
