using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Domain.Repositories
{
    public interface ILocalizationService
    {
        IReadOnlyList<string> SupportedLanguages { get; }
        string Language { get; }
    }
}
