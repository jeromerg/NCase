using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NCaseFramework.doc
{
    public class SnippetUtil
    {
        private readonly Regex mSnippetRegex;
        private readonly Dictionary<string, string> mSnippets = new Dictionary<string, string>();

        public SnippetUtil(string snippetMarker = "//#",
                           string snippetRegexString = @"(?<={0}\s+(?<name>.*?\w)[\W]*\r\n)(?<body>.*?(\r\n)?)(?=([\t ]*{0}))")
        {
            string inflatedSnippetRegexString = string.Format(snippetRegexString, snippetMarker);
            mSnippetRegex = new Regex(inflatedSnippetRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            ;
        }

        public void ParseSnippets(string txtContainingSnippets)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            MatchCollection blocks = mSnippetRegex.Matches(txtContainingSnippets);

            Console.WriteLine("Found {0} snippet(s)", blocks.Count);
            foreach (Match block in blocks)
            {
                string snippetName = block.Groups["name"].Captures[0].Value.Trim();
                string snippetBody = block.Groups["body"].Captures[0].Value;

                Console.WriteLine("Snippets: '{0}'", snippetName);

                string unindentedBody = TextUtil.Desindent(snippetBody);

                if (mSnippets.ContainsKey(snippetName))
                    throw new ArgumentException("Snippet '{0}' defined multiple times", snippetName);

                mSnippets.Add(snippetName, unindentedBody);
            }
        }

        public string SubstituteSnippets(string txtContainingSnippets, SnippetUtil ersatzSnippets)
        {
            return mSnippetRegex.Replace(txtContainingSnippets, match => ReplaceSnippet(ersatzSnippets, match));
        }

        private string ReplaceSnippet(SnippetUtil ersatzSnippets, Match match)
        {
            string snippetName = match.Groups["name"].Captures[0].Value.Trim();

            string ersatzSnippet;
            if (!ersatzSnippets.mSnippets.TryGetValue(snippetName, out ersatzSnippet))
            {
                string msg = string.Format("Snippet '{0}' not found in ersatzSnippets. List of available Snippets: {1}",
                                           snippetName,
                                           string.Join(", ", ersatzSnippets.mSnippets.Keys));
                throw new ArgumentException(msg);
            }
            return ersatzSnippet;
        }
    }
}