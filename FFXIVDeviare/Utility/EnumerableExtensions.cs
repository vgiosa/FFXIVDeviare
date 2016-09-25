using System.Collections.Generic;

namespace FFXIVDeviare.Utility
{
    public static class EnumerableExtensions
    {
        public static HashSet<T> ToHashset<T>(this IEnumerable<T> source)
        {
            var hashset = new HashSet<T>(source);
            return hashset;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            var queue = new Queue<T>(source);
            return queue;
        }
    }
}
