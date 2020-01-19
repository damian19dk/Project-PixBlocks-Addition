using Microsoft.Extensions.Options;
using PixBlocks_Addition.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PixBlocks_Addition.Domain.Entities
{
    public static class Languages
    {
        private static readonly Regex regex_language = new Regex("[A-Za-z]+");

        public static string Polish => "pl";
        public static string English => "en";
        public static string None => "none";

        public static bool IsValidLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                return false;
            }
            if (!regex_language.IsMatch(language))
            {
                return false;
            }
            if (language.Length < 2 || language.Length > 60)
            {
                return false;
            }
            return true;
        }
    }
}
