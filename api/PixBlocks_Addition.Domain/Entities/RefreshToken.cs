using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = token;
        }

        public void Revoke(string language)
        { 
            if (Revoked)
            {
                if (language == "en")
                    throw new MyException(MyCodesNumbers.RefreshTokenAllready, $"Refresh token: '{Id}' was already revoked '{RevokedAt}'.");
                else
                    throw new MyException(MyCodesNumbers.RefreshTokenAllready, $"Refresh token: '{Id}' był już odwołany w '{RevokedAt}'.");
            }
            Revoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }

}
