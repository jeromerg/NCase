using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using NUtil.Text;

namespace NUtil.Doc
{
    public class SnippetParser
    {
        [NotNull] private readonly Regex mSnippetRegex;
        [CanBeNull] private readonly Regex mExcludedLineRegex;

        public SnippetParser(Regex snippetMarkerRegex, [CanBeNull] Regex excludedLineRegex)
        {
            mSnippetRegex = snippetMarkerRegex;
            mExcludedLineRegex = excludedLineRegex;
        }

        public List<Snippet> ParseFileSnippets(string filePath)
        {
            Console.WriteLine("Extracting snippets of '{0}'", filePath);
            string fileContent = System.IO.File.ReadAllText(filePath);
            List<Snippet> snippets = ParseSnippets(string.Format("File '{0}'", filePath), fileContent);
            return snippets;
        }

        public List<Snippet> ParseSnippets(string source, string txtContainingSnippets)
        {
            var snippets = new List<Snippet>();

            // ReSharper disable once AssignNullToNotNullAttribute
            MatchCollection blocks = mSnippetRegex.Matches(txtContainingSnippets);

            Console.WriteLine("Found {0} snippet(s)", blocks.Count);
            foreach (Match block in blocks)
            {
                string snippetName = block.Groups["name"].Captures[0].Value.Trim();
                string snippetBody = block.Groups["body"].Captures[0].Value;

                Console.WriteLine("Snippet: '{0}'", snippetName);

                string unindentedBodyLines = TextExtensions.Desindent(snippetBody);

                IEnumerable<string> unindentedAndIncludedLines = unindentedBodyLines
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                    .Where(line => mExcludedLineRegex == null || !mExcludedLineRegex.IsMatch(line));

                string snippetFinalBody = string.Join(Environment.NewLine, unindentedAndIncludedLines);

                snippets.Add(new Snippet(source, snippetName, snippetFinalBody));
            }

            return snippets;
        }

        public void SubstituteFileSnippets(string filePath, Dictionary<string, Snippet> ersatzSnippets)
        {
            string fileContent = System.IO.File.ReadAllText(filePath);
            string newContent = SubstituteSnippets(fileContent, ersatzSnippets);

            if (fileContent == newContent)
            {
                Console.WriteLine("Document didn't change. No update performed");
                return;
            }

            Console.WriteLine("Document changed. Saving new document file content");
            System.IO.File.Delete(filePath); // TODO DELETE USING WINDOWS SAFE DELETE FUNCTION
            System.IO.File.WriteAllText(filePath, newContent);
        }

        public string SubstituteSnippets(string txtContainingSnippets, Dictionary<string, Snippet> ersatzSnippets)
        {
            return mSnippetRegex.Replace(txtContainingSnippets, match => ReplaceSnippet(ersatzSnippets, match));
        }

        private string ReplaceSnippet(Dictionary<string, Snippet> ersatzSnippets, Match match)
        {
            string snippetName = match.Groups["name"].Captures[0].Value.Trim();
            string snippetBody = match.Groups["body"].Captures[0].Value;

            Snippet ersatzSnippet;
            if (!ersatzSnippets.TryGetValue(snippetName, out ersatzSnippet))
            {
                string msg = string.Format("Snippet '{0}' not found in ersatzSnippets. List of available Snippets: {1}",
                                           snippetName,
                                           string.Join(", ", ersatzSnippets.Keys));
                throw new ArgumentException(msg);
            }

            string trimmedSnippet = ersatzSnippet.Body.Trim();
            if (snippetBody != trimmedSnippet)
                Console.WriteLine("Snippet '{0}' changed... upgrading it", snippetName);
            else
                Console.WriteLine("Snippet '{0}' didn't change", snippetName);

            return trimmedSnippet;
        }
    }
}