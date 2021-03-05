using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public static class LINQExtension
    {
        public static float Median(this IEnumerable<float> source)
        {
            if (!(source?.Any() ?? false))
            {
                throw new InvalidOperationException("Source is null");
            }

            var sortedList = source.OrderBy(item => item).ToList();

            int itemIndex = sortedList.Count / 2;

            return sortedList.Count % 2 == 0
                ? (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2
                : sortedList[itemIndex];
        }
    }
}
