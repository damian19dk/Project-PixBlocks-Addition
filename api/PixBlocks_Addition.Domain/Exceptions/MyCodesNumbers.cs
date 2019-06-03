using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    public class MyCodesNumbers
    {
        //kody z 1*** dotyczą uzytkownika
        public static string InvalidCredentials => "1000";
        public static string WrongFormatOfMail => "1001";
        public static string TooLongLogin => "1002";
        public static string TooShortLogin => "1003";
        public static string TooLongPassword => "1004";
        public static string TooShortPassword => "1005";
        public static string WrongRoleId => "1006";
        public static string WrongUserStatus => "1007";
        public static string WrongCharactersInPassword => "1008";
        public static string WrongCharactersInLogin => "1009";
        public static string SamePassword => "1010";
        public static string SameEmail => "1011";
        public static string UniqueEmail => "1012";
        public static string UniqueLogin => "1013";
        //kody z 2*** dotyczą treści
        public static string TooShortCourseName => "2000";
        public static string TooLongDescription => "2001";
        public static string SameVideo => "2002";
        public static string LessonNotFound => "2003";
        public static string VideoNotFound => "2004";
        public static string ExerciseNotFound => "2005";
        public static string CourseNotFound => "2006";
        public static string WrongFormatOfPhoto => "2007";
        public static string ImageNotFound => "2008";
        public static string SameTitleCourse => "2009";
        public static string SameTitleExercise => "2010";
        public static string SameTitleLesson => "2011";
        //kody z 3*** dotycza JWT
        public static string TokenNotFound => "3000";
        public static string UserNotFoundJWT => "3001";
        public static string RefreshAToken => "3002";
        public static string RefreshToken => "3003";
        //JWPlayer
        public static string CouldNotUploadVideo => "4000";
        //Resources
        public static string MissingFile => "5000";
        //Domain
        public static string InvalidTitle => "6001";
        public static string InvalidDescription => "6002";
        public static string InvalidPictureSrc => "6003";
        public static string InvalidDuration => "6004";
        public static string InvalidLanguage => "6005";
    }
}
