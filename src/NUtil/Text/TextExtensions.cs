using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUtil.Text
{
    public static class TextExtensions
    {
        public static IEnumerable<string> Lines(this string s)
        {
            string[] lines = s.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            return lines;
        }

        public static string JoinLines(this IEnumerable<string> lines)
        {
            string joined = string.Join(Environment.NewLine, lines);
            return joined;
        }

        public static string Desindent(this string txt)
        {
            string[] lines = txt.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            if (lines.Length == 0)
                return txt;

            int indentMin = lines.Min(l => GetIndent(l));

            var sb = new StringBuilder();
            bool isFirstLine = true;
            foreach (string line in lines)
            {
                if (!isFirstLine)
                    sb.AppendLine();
                isFirstLine = false;
                sb.Append(line.Length >= indentMin ? line.Substring(indentMin) : "");
            }
            return sb.ToString();
        }

        private static int GetIndent(string s)
        {
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