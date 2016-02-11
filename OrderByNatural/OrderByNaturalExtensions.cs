using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.LandonKey.OrderByNatural
{
    public static class OrderByNaturalExtensions
    {
        public static IEnumerable<T> OrderByNatural<T>(this IEnumerable<T> objects, Func<T, string> func)
        {
            Func<string, object> convert = str =>
            {
                int x = 0;
                if (int.TryParse(str, out x))
                    return x;
                else
                    return str;
            };

            return objects.OrderBy(x =>
                Regex.Split(func(x), "([0-9]+)").Select(convert),
                new EnumerableComparer<object>());
        }

        public static IEnumerable<T> OrderByNaturalDesc<T>(this IEnumerable<T> objects, Func<T, string> func)
        {
            return objects.OrderByNatural(func).Reverse();
        }
    }
}
