using System;
using System.Collections.Generic;
using System.Text;

namespace ERV.App.Infrastructure.Extensions
{
    public static class StringsExtension
    {
        public static string CleanSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str.Trim();
        }
        public static string ReplaceAll(this string str)
        {
            return str.Replace("{", "").Replace("}", "").Replace("\"","");
        }
    }
}
