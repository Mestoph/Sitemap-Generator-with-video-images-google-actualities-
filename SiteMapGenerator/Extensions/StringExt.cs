using System;

namespace SiteMapGenerator.Extensions
{
    internal static class StringExt
    {
        public static bool ContainsIgnoreCase(this string source, string substring)
        {
            return string.IsNullOrEmpty(substring) ? false : source?.IndexOf(substring, StringComparison.InvariantCultureIgnoreCase) > -1;
        }
    }
}
