using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMock
{
    public static class EnumerableExtensions
    {
        public static bool ListEqual<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            var e1 = source.GetEnumerator();
            var e2 = other.GetEnumerator();

            bool b1 = e1.MoveNext();
            bool b2 = e2.MoveNext();

            while (b1 && b2)
            {
                if (!Equals(e1.Current, e2.Current)) return false;
                b1 = e1.MoveNext();
                b2 = e2.MoveNext();
            }

            return !(b1 || b2);
        }
    }
}
