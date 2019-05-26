using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    public class MyCodes
    {
        public static string WrongFormatOfMail => "Zły format adresu e-mail!"; //1001
        public static string TooLongLogin => "Login jest za długi! (max 20 znaków)"; //1002
        public static string TooShortLogin => "Login jest za krótki! (min 4 znaki)"; //1003
        public static string TooLongPassword => "Hasło jest za długie! (max 20 znaków)"; //1004
        public static string TooShortPassword => "Hasło jest za krótkie! (min 6 znaków)"; //1005
        public static string WrongRoleId => "Niewłaściwe Id roli!"; //1006
        public static string WrongUserStatus => "Niewłasciwy status użytkownika!"; //1007
        public static string WrongCharactersInPassword => "Niedozwolone znaki w haśle!"; //1008
        public static string WrongCharactersInLogin => "Niedozwolone znaki w loginie!"; //1009
        public static string InvalidImage => "invalid_image";
        public static string ImageNotFound => "image_not_found";
        public static string TooShortCourseName => "Za krótka nazwa kursu!"; //2000
        public static string TooLongDescription => "Za długi opis!"; //2001
        public static string SamePassword => "Nowe hasło musi być różne od starego!"; //1010
        public static string SameEmail => "Nowy e-mail musi być różny od starego!"; //1011
        public static string UniqueEmail => "Taki e-mail istnije już w bazie!"; //1012
        public static string UniqueLogin => "Konto o takim loginie istnieje już w bazie!"; //1013
        public static string InvalidCredentials => "Niepoprawne dane logowania!"; //1000
        public static string TokenNotFound => "Refresh token was not found!"; //3000
        public static string UserNotFoundJWT => "User not found!"; //3001
        public static string RefreshAToken => "Refresh token was already revoked!"; //3002
        public static string RefreshToken => "Refresh token was revoked!"; //3003
        public static string SameVideo => "The lesson already has the same video!"; //2002
        public static string LessonNotFound => "Nie znalezniono lekcji!"; //2003
        public static string VideoNotFound => "Nie znaleziono wideo!"; //2004
        public static string ExerciseNotFound => "Nie znaleziono ćwiczenia!"; //2005

    }
}
