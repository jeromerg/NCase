using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NUtil.Linq;

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

            return string.Join(Environment.NewLine, lines);
        }

        [NotNull]
        public static string Desindent([NotNull] this string txt, int tabIndentation)
        {
            if (txt == null) throw new ArgumentNullException("txt");

            string[] lines = txt.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            if (lines.Length == 0)
                return txt;

            // ReSharper disable once AssignNullToNotNullAttribute
            int indentMin = lines.Min(line => GetIndent(line, tabIndentation));

            string desindentedTxt = lines
                // ReSharper disable once PossibleNullReferenceException
                .Select(line => line.Length >= indentMin ? line.Substring(indentMin) : "")
                .JoinLines();

            return desindentedTxt;
        }

        [NotNull]
        public static string Indent([NotNull] this string txt, int indentation)
        {
            if (txt == null) throw new ArgumentNullException("txt");

            var sb = new StringBuilder();

            txt.Lines().ForEach(l =>
                                {
                                    sb.Append(new string(' ', indentation));
                                    sb.Append(l);
                                },
                                () => sb.AppendLine());

            return sb.ToString();
        }

        private static int GetIndent([NotNull] string s, int tabIndentation)
        {
            if (s == null) throw new ArgumentNullException("s");

            int result = 0;
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\t':
                        result += tabIndentation;
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