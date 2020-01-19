using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixBlocks_Addition.Tests.EndToEnd.Extentions
{
    public static class DtoExtentions
    {
        private static string[] arrayProperties = { "Resources" };
        private static string[] useToStringForProperties = { "Id", "Premium", "PublishDate", "Duration" };
        private static string[] noMapProperties = { "Courses", "Lessons", "Exercises", "Videos",
                                                    "CourseVideos", "LessonVideos", "ExerciseVideos", "PublishDate"};
        public static Dictionary<string, string> GetProperties<MediaDto, TResult>(this MediaDto lesson, Func<MediaDto, TResult> func)
        {
            var parameters = func(lesson);
            var properties = parameters.GetType().GetProperties();
            var dict = new Dictionary<string, string>();

            foreach (var p in properties)
            {
                if(p.Name == "Tags")
                {
                    var tagsDto = ((IEnumerable<TagDto>)p.GetValue(parameters));
                    ISet<string> tags = new HashSet<string>();
                    foreach (var t in tagsDto)
                        tags.Add(t.Name);
                    dict.Add(p.Name, string.Join(',', tags));
                }
                else if (arrayProperties.Contains(p.Name))
                {
                    dict.Add(p.Name, string.Join(',', (IEnumerable<string>)p.GetValue(parameters)));
                }
                else if(useToStringForProperties.Contains(p.Name))
                {
                    dict.Add(p.Name, p.GetValue(parameters).ToString());
                }
                else if(p.Name=="Picture")
                {
                    dict.Add("PictureUrl", (string)p.GetValue(parameters));
                }
                else
                {
                    dict.Add(p.Name, (string)p.GetValue(parameters));
                }
            }

            return dict;
        }

        public static Dictionary<string, string> GetProperties<MediaDto>(this MediaDto lesson)
        {
            var properties =  typeof(Infrastructure.DTOs.MediaDto).GetProperties();
            var dict = new Dictionary<string, string>();

            foreach (var p in properties)
            {
                if (p.Name == "Tags")
                {
                    var tagsDto = ((IEnumerable<TagDto>)p.GetValue(lesson));
                    ISet<string> tags = new HashSet<string>();
                    foreach (var t in tagsDto)
                        tags.Add(t.Name);
                    dict.Add(p.Name, string.Join(',', tags));
                }
                else if (arrayProperties.Contains(p.Name))
                {
                    dict.Add(p.Name, string.Join(',', (IEnumerable<string>)p.GetValue(lesson)));
                }
                else if (useToStringForProperties.Contains(p.Name))
                {
                    dict.Add(p.Name, p.GetValue(lesson).ToString());
                }
                else if (p.Name == "Picture")
                {
                    dict.Add("PictureUrl", (string)p.GetValue(lesson));
                }
                else
                {
                    dict.Add(p.Name, (string)p.GetValue(lesson));
                }
            }

            return dict;
        }
    }
}
