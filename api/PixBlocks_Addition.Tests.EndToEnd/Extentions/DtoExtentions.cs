using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixBlocks_Addition.Tests.EndToEnd.Extentions
{
    public static class DtoExtentions
    {
        private static string[] arrayProperties = { "Tags" };
        private static string[] useToStringForProperties = { "Id", "Premium", "PublishDate", "Duration" };

        public static Dictionary<string, string> GetProperties<MediaDto, TResult>(this MediaDto lesson, Func<MediaDto, TResult> func)
        {
            var parameters = func(lesson);
            var properties = parameters.GetType().GetProperties();
            var dict = new Dictionary<string, string>();

            foreach (var p in properties)
            {
                if (arrayProperties.Contains(p.Name))
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
    }
}
