using System;
using System.Linq;
using System.Text;

namespace NCaseFramework.doc
{
    public class TextUtil
    {
        public static string Desindent(string txt)
        {
            string[] lines = txt.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            if (lines.Length == 0)
                return txt;

            int indentMin = lines.Min(l => GetIndent(l));

            var sb = new StringBuilder();
            foreach (string line in lines)
            {
                sb.AppendLine(line.Length >= indentMin ? line.Substring(indentMin) : "");
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