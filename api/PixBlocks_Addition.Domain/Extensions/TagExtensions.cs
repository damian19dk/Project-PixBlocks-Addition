using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Extensions
{
    public static class TagExtensions
    {
        public static bool CheckLanguage(this Tag tag, string language)
        {
            if (tag.Language == Languages.None)
                return true;
            else
                return String.Equals(tag.Language, language, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
