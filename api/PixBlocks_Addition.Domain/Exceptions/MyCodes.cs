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
        public static string ImageNotFound => "Nie znaleziono zdjęcia!";//2010
        public static string TooLongDescription => "Opis nie może mieć wiecej niż 10000 znaków!"; //2001
        public static string TooShortDescription => "Opis nie może mieć mniej niż 3 znaki!";//2000
        public static string SamePassword => "Nowe hasło musi być różne od starego!"; //1010
        public static string SameEmail => "Nowy e-mail musi być różny od starego!"; //1011
        public static string UniqueEmail => "Taki e-mail istnije już w bazie!"; //1012
        public static string UniqueLogin => "Konto o takim loginie istnieje już w bazie!"; //1013
        public static string InvalidCredentials => "Niepoprawne dane logowania!"; //1000
        public static string TokenNotFound => "Refresh token was not found!"; //3000
        public static string UserNotFoundJWT => "User not found!"; //3001
        public static string RefreshAToken => "Refresh token was already revoked!"; //3002
        public static string RefreshToken => "Refresh token was revoked!"; //3003
        public static string SameVideoInLesson => "The lesson already has the same video!"; //2002
        public static string LessonNotFound => "Nie znalezniono lekcji!"; //2003
        public static string VideoNotFound => "Nie znaleziono wideo!"; //2004
        public static string ExerciseNotFound => "Nie znaleziono ćwiczenia!"; //2005
        public static string CourseNotFound => "Nie znaleziono kursu!"; //2006
        public static string SameVideoInCourse => "Kurs posiada już takie wideo!";//2002
        public static string SameVideoInExercise => "Ćwiczenie posiada już takie wideo!";//2002
        public static string EmptyLanguageTitle => "Język nie może być pusty!";//6004
        public static string WrongCharactersInLanguage => "Język zawiera niepoprawne znaki!";//6001
        public static string TooShortLanguage => "Język musi mieć co najmniej 2 znaki!";//6002
        public static string TooLongLanguage => "Język nie może mieć więcej niż 60 znaków!";//6003
        public static string InvalidDuration => "Czas trwania nie może być ujemny!";//6000
        public static string InvalidPicSource => "Źródło zdjęcia musi być URL lub id!";//2019
        public static string EmptyTitle => "Tytuł nie może być pusty!";//2018
        public static string TooShortTitle => "Tytuł musi zawierać co najmniej 3 znaki!";//2016
        public static string TooLongTitle => "Tytuł nie może zawierać więcej niż 250 znaków!";//2017
        public static string NegativeLength => "Długość kursu nie może być ujemna!";//2025
        public static string UserNotFound => "Nie ma takiego użytkownika!";//1014
        public static string VideoHistoryNotFound => "Nie znaleziono histori wideo użytkonika!";//1015
        public static string VideoInHistory => "To wideo jest już w historii użytkonika!";//1016
        public static string NegativeTime => "Czas nie może być ujemny!";//2026
        public static string MissingVideos => "Brak wideo w kursie!";//2027
        public static string WrongUserId => "Nie ma użytkownika o takim Id!";//1014
        public static string MissingUser => "Nie ma takiego użytkownika!";//1015
        public static string MissingCourse => "Nie ma takiego kursu!";//2025
        public static string MissingHistory => "Brak historii użytkownika!";//1016
        public static string SameCourseInUserHistory => "Kurs już znajduje się w histori użytkownika!";//1017
    }
}
