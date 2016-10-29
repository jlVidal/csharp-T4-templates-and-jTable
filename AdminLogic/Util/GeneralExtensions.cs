using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLogic.Util
{
    public static class GeneralExtensions
    {
        public static string ToSqlString(this ListSortDirection direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                return "ASC";
            }

            return "DESC";
        }

        public static string[] GetFirstGenericTypePropNames(Type targetType)
        {
            var genericType = targetType.GetGenericArguments();

            if (genericType.Length <= 0)
                return new string[0];

            var pocoType = genericType[0];
            var allProps = pocoType.GetProperties();

            return allProps.Select(a => a.Name).ToArray();
        }

        public static string Capitalize(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            if (char.IsUpper(text[0]))
                return text;

            var firstLetter = char.ToUpper(text[0]);
            var otherLetters = text.Skip(1);

            var concatenation = new[] {firstLetter}.Concat(otherLetters).ToArray();
            return new string(concatenation);

        }

        
    }
}
