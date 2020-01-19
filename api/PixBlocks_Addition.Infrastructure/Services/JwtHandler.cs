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
    public class JwtHandler : IJwtPlayerHandler, IJwtHandler
    {
        private readonly JwtOptions _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> jwtSettings)
        {
            _jwtOptions = jwtSettings.Value;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateLifetime = _jwtOptions.ValidateLifetime
            };
        }

        public JwtHandler(IOptions<JWPlayerOptions> jwPlayerSettings)
        {
            _jwtOptions =
                new JwtOptions()
                {
                    ExpiryMinutes = jwPlayerSettings.Value.ExpiryMinutes,
                    Issuer = jwPlayerSettings.Value.Issuer,
                    SecretKey = jwPlayerSettings.Value.SecretKey,
                    ValidateLifetime = false
                };
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateLifetime = _jwtOptions.ValidateLifetime
            };
        }

        public JsonWebToken Create(Guid userId, string login, string role, bool isPremium,
            IDictionary<string, string> claims = null)
        {
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("premium", isPremium.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    jwtClaims.Add(new Claim(claim.Key, claim.Value));
                }
            }

            return createJwt(jwtClaims);
        }

        public string Create(string resource)
        {
            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim("resource", resource)
            };

            var token = createJwt(jwtClaims);
            return token.AccessToken;
        }

        private JsonWebToken createJwt(IEnumerable<Claim> jwtClaims)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                claims: jwtClaims,
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
