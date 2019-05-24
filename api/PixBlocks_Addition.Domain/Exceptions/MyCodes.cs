using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    public class MyCodes
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
        public static string SamePassword => "Nowe hasło musi być różne od starego!";
        public static string SameEmail => "Nowy e-mail musi być różny od starego!";
        public static string UniqueEmail => "Taki e-mail istnije już w bazie!";
        public static string UniqueLogin => "Konto o takim loginie istnieje już w bazie!";
        public static string InvalidCredentials => "Niepoprawne dane logowania!";
        public static string TokenNotFound => "Refresh token was not found!";
        public static string UserNotFounJWT => "User not found!";
        public static string RefreshAToken => "Refresh token was already revoked!";
        public static string RefreshToken => "Refresh token was revoked!";

    }
}
