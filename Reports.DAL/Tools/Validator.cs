using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Reports.DAL.Tools
{
    public static class Validator
    {
        public static void NotNull(this object argument, string message)
        {
            if (argument == null)
                throw new ArgumentNullException(string.Empty, message);
        }

        public static void NotEmpty<T>(this IEnumerable<T> argument, string message)
        {
            if (argument.ToList().Count == 0)
                throw new ReportsException(message);
        }

        public static void WithoutNull<T>(this IEnumerable<T> argument, string message)
        {
            if (argument.GetType().GetGenericArguments()[0] == typeof(string))
            {
                if (argument.Any(item => string.IsNullOrEmpty(item.ToString())))
                    throw new ArgumentNullException(string.Empty, message);
            }

            if (argument.Any(item => item == null || string.IsNullOrEmpty(item.ToString())))
                throw new ArgumentNullException(string.Empty, message);
        }

        public static void WithoutDuplicates<T>(this IEnumerable<T> argument, T item, string message)
        {
            if (argument.Any(arg => arg.Equals(item)))
                throw new DuplicateNameException(message);
        }
    }
}