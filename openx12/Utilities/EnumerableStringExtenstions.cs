using System.Collections.Generic;

namespace openx12.Utilities {
    public static class EnumerableStringExtenstions {
        public static string JoinStrings<T>(this IEnumerable<T> strings, string delimiter) {
            return string.Join(delimiter, strings);
        }
    }
}
