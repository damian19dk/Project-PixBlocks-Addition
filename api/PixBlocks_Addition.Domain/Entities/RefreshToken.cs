using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; protected set; }
        public string Token { get; protected set; }
        public bool Revoked { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? RevokedAt { get; protected set; }

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, string token)
        {
            Id = Guid.NewGuid();
            UserId = user.UserId;
            CreatedAt = DateTime.UtcNow;
            Token = token;
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new Exception($"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }
            Revoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }

}
