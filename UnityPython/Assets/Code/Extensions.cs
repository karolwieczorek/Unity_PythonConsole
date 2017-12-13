using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityPython.Assets.Code {
    public static class Extensions {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N) {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static int LinesCount(this string str) {
            if (str == null)
                throw new ArgumentNullException("str");
            if (str == string.Empty)
                return 0;
            int index = -1;
            int count = 0;
            while (-1 != (index = str.IndexOf("\n", index + 1)))
                count++;

            return count + 1;
        }
    }
}
