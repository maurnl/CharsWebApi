using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Extensions
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            Span<char> destination = stackalloc char[1];
            str.AsSpan(0, 1).ToUpperInvariant(destination);
            return $"{destination}{str.AsSpan(1)}";
        }
    }
}
