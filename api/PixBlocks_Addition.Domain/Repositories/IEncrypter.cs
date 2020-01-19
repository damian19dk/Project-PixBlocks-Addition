using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface IEncrypter
    {
        string GetSalt(string values);
        string GetHash(string vaule, string salt);
    }
}
