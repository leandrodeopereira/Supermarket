namespace SupermarketApi.Extensions
{
    using System;
    using System.Linq;

    public static class StringExstensions
    {
        public static string AddSpaceBeforeCapitalLetters(this string str)
        {
            _ = str ?? throw new ArgumentNullException(nameof(str));

            return str = string.Concat(str.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
    }
}
