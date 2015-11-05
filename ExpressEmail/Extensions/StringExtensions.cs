using System;
using System.Text.RegularExpressions;

namespace ExpressEmail.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmail(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            const string pattern = "^(?!\\.)(\"([^\"\\r\\\\]|\\\\[\"\\r\\\\])*\"|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(value);

        } 
    }
}