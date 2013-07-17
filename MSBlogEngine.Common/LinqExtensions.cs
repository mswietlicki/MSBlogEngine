using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
    }
}