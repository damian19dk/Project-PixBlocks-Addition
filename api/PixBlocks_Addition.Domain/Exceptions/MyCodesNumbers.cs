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
        public static string UserNotFound => "1014";
        public static string VideoHistoryNotFound => "1015";
        public static string VideoInHistory => "1016";
        public static string WrongUserId => "1014";
        public static string MissingUser => "1015";
        public static string MissingHistory => "1016";
        public static string SameCourseInUserHistory => "1017";
        //kody z 2*** dotyczą treści
        public static string TooShortDescription => "2000";
        public static string TooLongDescription => "2001";
        public static string SameVideoInLesson => "2002";
        public static string SameVideoInCourse => "2003";
        public static string SameVideoInExercise => "2004";
        public static string LessonNotFound => "2005";
        public static string VideoNotFound => "2006";
        public static string ExerciseNotFound => "2007";
        public static string CourseNotFound => "2008";
        public static string WrongFormatOfPhoto => "2009";
        public static string ImageNotFound => "2010";
        public static string SameTitleCourse => "2011";
        public static string SameTitleExercise => "2012";
        public static string SameTitleLesson => "2013";
        public static string SameTitleMedia => "2014";
        public static string MediaNotFound => "2015";
        public static string TooShortTitle => "2016";
        public static string TooLongTitle => "2017";
        public static string EmptyTitle => "2018";
        public static string InvalidPictureSrc => "2019";
        public static string InvalidDescription => "2020";
        public static string InvalidTitle => "2021";
        public static string SameVideo => "2022";
        public static string InvalidVideoParentId => "2023";
        public static string InvalidOrderData => "2024";
        public static string NegativeLength => "2025";
        public static string NegativeTime => "2026";
        public static string MissingVideos => "2027";
        public static string MissingCourse => "2025";
        public static string AmbiguousTitle => "2026";
        public static string QuizNotFound => "2027";
        public static string QuizExists => "2028";
        public static string TagExists => "2029";
        public static string TagNotFound => "2030";
        //kody z 3*** dotycza JWT
        public static string TokenNotFound => "3000";
        public static string UserNotFoundJWT => "3001";
        public static string RefreshAToken => "3002";
        public static string RefreshToken => "3003";
        public static string RefreshTokenAllready => "3004";
        //JWPlayer
        public static string CouldNotUploadVideo => "4000";
        //Resources
        public static string MissingFile => "5000";
        //Domain
        public static string InvalidDuration => "6000";
        public static string InvalidLanguage => "6001";
        public static string TooShortLanguage => "6002";
        public static string TooLongLanguage => "6003";
        public static string EmptyLanguage => "6004";
        public static string CouldntLoad => "6005";
        public static string InvalidIndex => "6006";
        public static string InvalidTagName => "6007";
        public static string InvalidColor => "6008";
    }
}
