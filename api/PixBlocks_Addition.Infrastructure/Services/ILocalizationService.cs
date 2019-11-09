using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public interface ILocalizationService
    {
        IReadOnlyList<string> supportedLanguages { get; }
        string GetCurrentLanguage();
    }
}
