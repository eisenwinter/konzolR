using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Utility
{
    internal static class ContainerExtensions
    {
        public static bool NotNullAndAny<TSource>(this IEnumerable<TSource> source)
        {
            return source != null && source.Any();
        }

        public static bool NotNullAndAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source != null && source.Any(predicate);
        }
    }
}
