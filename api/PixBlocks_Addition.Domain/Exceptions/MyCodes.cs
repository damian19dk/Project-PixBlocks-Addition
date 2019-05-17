using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    class MyCodes
    {
        public static string WrongFormatOfMail => "Zły format adresu e-mail!";
        public static string TooLongLogin => "Login jest za długi! (max 20 znaków)";
        public static string TooShortLogin => "Login jest za krótki! (min 4 znaki)";
        public static string TooLongPassword => "Hasło jest za długie! (max 20 znaków)";
        public static string TooShortPassword => "Hasło jest za krótkie! (min 6 znaków)";
        public static string WrongRoleId => "Niewłaściwe Id roli!";
        public static string WrongUserStatus => "Niewłasciwy status użytkownika!";
        public static string WrongCharactersInPassword => "Niedozwolone znaki w haśle!";
        public static string WrongCharactersInLogin => "Niedozwolone znaki w loginie!";
        public static string TooShortCourseName => "Za krótka nazwa kursu!";
        public static string TooLongDescription => "Za długi opis!";

    }
}
