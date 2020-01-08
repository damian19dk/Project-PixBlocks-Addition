using PixBlocks_Addition.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixBlocks_Addition.Domain.Extensions
{
    public static class CollectionExtension
    {
        public static void Sort<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach(var element in sortedList)
            {
                source.Add(element);
            }
        }
    }
}
