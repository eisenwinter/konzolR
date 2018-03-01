using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbkst.konzolR.Ui.Utility
{
    internal static class NumberUtility
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable
        {
            T output = value;
            if (value.CompareTo(max) > 0)
            {
                return max;
            }
            if (value.CompareTo(min) < 0)
            {
                return min;
            }
            return output;
        }

        /// <summary>
        /// clamps a index into a valid range for a collection
        /// where everything greater than the the highest index will become zero
        /// and everything smaller than 0 will be the highest index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index">index to check</param>
        /// <returns></returns>
        public static int IndexRoundAbout<T>(this ICollection<T> collection, int index)
        {
            if(index >= 0 && index <= (collection.Count - 1))
            {
                return index;
            }
            if(index < 0)
            {
                return collection.Count - 1;
            }
            return 0;
        }

        /// <summary>
        /// returns the next element within the collection, if roundabout is set it will start over
        /// at the first element when the end is reached
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="item">item we want to get the next neighbour of</param>
        /// <param name="roundAbout">if set to true starts over at the end</param>
        /// <returns>the element or the default value if the end is reached without roundabout</returns>
        public static T Next<T>(this IEnumerable<T> collection, T item, bool roundAbout = false)
        {
            if (!roundAbout)
            {
                var tmp = collection.ToList();
                int idx = tmp.IndexOf(item) + 1;
                if (idx < tmp.Count)
                {
                    return tmp[idx];
                }
                return default(T);
            }
            else
            {
                var tmp = collection.ToList();
                int idx = tmp.IndexOf(item) + 1;
                idx = tmp.IndexRoundAbout(idx);
                return tmp[idx];
            }
        }

        /// <summary>
        /// returns the previous element within the collection, if roundabout is set it will start over
        /// at the last element when the first one is reached
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="item">item we want to get the next neighbour of</param>
        /// <param name="roundAbout">if set to true starts over at the end</param>
        /// <returns>the element or the default value if the end is reached without roundabout</returns>
        public static T Previous<T>(this IEnumerable<T> collection, T item, bool roundAbout = false)
        {
            if (!roundAbout)
            {
                var tmp = collection.ToList();
                int idx = tmp.IndexOf(item) - 1;
                if (idx >= 0)
                {
                    return tmp[idx];
                }
                return default(T);
            }
            else
            {
                var tmp = collection.ToList();
                int idx = tmp.IndexOf(item) - 1;
                idx = tmp.IndexRoundAbout(idx);
                return tmp[idx];
            }
        }

    }
}
