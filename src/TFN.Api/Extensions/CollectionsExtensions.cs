using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFN.Api.Extensions
{
    public static class CollectionsExtensions
    {
        public static IReadOnlyList<int> DownSampleTo(this IReadOnlyList<int> list, int newListSize)
        {
            if (newListSize >= list.Count)
            {
                throw new ArgumentException($"size of [{nameof(list)}] cannot be smaller than [{nameof(newListSize)}]");
            }

            var newList = new List<int>();
            var currentSum = 0;
            var bucketSize = (int)Math.Ceiling((decimal)list.Count / newListSize);

            for(var i = 0; i < list.Count; i++)
            {
                currentSum += list.ElementAt(i);
                if (i % bucketSize == 0)
                {
                    var value = (int)Math.Ceiling((decimal)currentSum / bucketSize);
                    newList.Add(value);
                    currentSum = 0;
                }
            }
            return newList;
        }
    }
}
