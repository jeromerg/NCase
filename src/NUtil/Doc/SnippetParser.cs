using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace NCaseFramework.doc
{
    public class SnippetParser
    {
        [NotNull] private readonly Regex mSnippetRegex;
        [CanBeNull] private readonly Regex mExcludedLineRegex;

        public SnippetParser(string snippetMarkerRegex, string excludedLineRegex = null)
            : this(snippetMarkerRegex,
                   excludedLineRegex,
                   @"(?<=^\s*{0}\s+(?<name>\w+)\W*?\r\n)(?<body>.*?)(?=\r\n[ \t]*(?:{0}|\Z))")
        {
        }

        public SnippetParser(string snippetMarkerRegex, [CanBeNull] string excludedLineRegex, string snippetRegexString)
        {
            string inflatedSnippetRegexString = string.Format(snippetRegexString, snippetMarkerRegex);
            mSnippetRegex = new Regex(inflatedSnippetRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
                
            mExcludedLineRegex = excludedLineRegex != null 
                ? new Regex(excludedLineRegex, RegexOptions.Singleline)
                : null;
        }

        public Dictionary<string, string> ParseSnippets(string txtContainingSnippets)
        {
            Dictionary<string, string> snippets = new Dictionary<string, string>();

            // ReSharper disable once AssignNullToNotNullAttribute
            MatchCollection blocks = mSnippetRegex.Matches(txtContainingSnippets);

            Console.WriteLine("Found {0} snippet(s)", blocks.Count);
            foreach (Match block in blocks)
            {
                string snippetName = block.Groups["name"].Captures[0].Value.Trim();
                string snippetBody = block.Groups["body"].Captures[0].Value;

                Console.WriteLine("Snippet: '{0}'", snippetName);

                if (snippets.ContainsKey(snippetName))
                    throw new ArgumentException(string.Format("Snippet '{0}' defined multiple times", snippetName));

                string unindentedBodyLines = TextUtil.Desindent(snippetBody);

                IEnumerable<string> unindentedAndIncludedLines = unindentedBodyLines
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                    .Where(line => mExcludedLineRegex == null || !mExcludedLineRegex.IsMatch(line));

                string snippetFinalBody = string.Join(Environment.NewLine, unindentedAndIncludedLines);

                snippets.Add(snippetName, snippetFinalBody);
            }

            return snippets;
        }

        public string SubstituteSnippets(string txtContainingSnippets, Dictionary<string, string> ersatzSnippets)
        {
            return mSnippetRegex.Replace(txtContainingSnippets, match => ReplaceSnippet(ersatzSnippets, match));
        }

        private string ReplaceSnippet(Dictionary<string, string> ersatzSnippets, Match match)
        {
            string snippetName = match.Groups["name"].Captures[0].Value.Trim();
            string snippetBody = match.Groups["body"].Captures[0].Value;

            string ersatzSnippet;
            if (!ersatzSnippets.TryGetValue(snippetName, out ersatzSnippet))
            {
                string msg = string.Format("Snippet '{0}' not found in ersatzSnippets. List of available Snippets: {1}",
                                           snippetName,
                                           string.Join(", ", ersatzSnippets.Keys));
                throw new ArgumentException(msg);
            }

            if(snippetBody != ersatzSnippet)
                Console.WriteLine("Snippet '{0}' changed... upgrading it", snippetName);
            else
                Console.WriteLine("Snippet '{0}' didn't change", snippetName);
            
            return ersatzSnippet;
        }
    }
}