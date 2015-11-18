using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace NUtil.Text
{
    public static class TextExtensions
    {
        [NotNull, ItemNotNull] 
        public static IEnumerable<string> Lines([NotNull] this string s)
        {
            if (s == null) throw new ArgumentNullException("s");
            string[] lines = s.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            return lines;
        }

        [NotNull] 
        public static string JoinLines([NotNull, ItemCanBeNull] this IEnumerable<string> lines)
        {
            if (lines == null) throw new ArgumentNullException("lines");
            string joined = string.Join(Environment.NewLine, lines);
            return joined;
        }

        [NotNull]
        public static string Desindent([NotNull] this string txt)
        {
            if (txt == null) throw new ArgumentNullException("txt");

            string[] lines = txt.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            if (lines.Length == 0)
                return txt;

            // ReSharper disable once AssignNullToNotNullAttribute
            int indentMin = lines.Min(line => GetIndent(line));

            var sb = new StringBuilder();
            foreach (string line in lines)
            {
                // ReSharper disable once PossibleNullReferenceException
                sb.Append(line.Length >= indentMin ? line.Substring(indentMin) : "");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static int GetIndent([NotNull] string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            int result = 0;
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\t':
                        result += 4;
                        break;
                    case ' ':
                        result += 1;
                        break;
                    default:
                        return result;
                }
            }
            return int.MaxValue;
        }
    }
}