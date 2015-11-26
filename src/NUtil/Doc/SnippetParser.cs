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

        public SnippetParser([NotNull] Regex snippetMarkerRegex, [CanBeNull] Regex excludedLineRegex)
        {
            if (snippetMarkerRegex == null) throw new ArgumentNullException("snippetMarkerRegex");

            mSnippetRegex = snippetMarkerRegex;
            mExcludedLineRegex = excludedLineRegex;
        }

        [NotNull]
        public List<Snippet> ParseFileSnippets([NotNull] string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");

            Console.WriteLine("Extracting snippets of '{0}'", filePath);
            string fileContent = System.IO.File.ReadAllText(filePath);
            List<Snippet> snippets = ParseSnippets(string.Format("File '{0}'", filePath), fileContent);
            return snippets;
        }

        [NotNull]
        public List<Snippet> ParseSnippets([NotNull] string source, [NotNull] string txtContainingSnippets)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (txtContainingSnippets == null) throw new ArgumentNullException("txtContainingSnippets");

            var snippets = new List<Snippet>();

            // ReSharper disable once AssignNullToNotNullAttribute
            MatchCollection blocks = mSnippetRegex.Matches(txtContainingSnippets);

            Console.WriteLine("Found {0} snippet(s)", blocks.Count);
            foreach (Match block in blocks)
            {
                string snippetName;
                string snippetBody;
                GetSnippetNameAndBody(block, out snippetName, out snippetBody);

                Console.WriteLine("Snippet: '{0}'", snippetName);

                string unindentedBodyLines = TextExtensions.Desindent(snippetBody);

                IEnumerable<string> unindentedAndIncludedLines = unindentedBodyLines
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                    .Where(line => line != null)
                    .Where(line => mExcludedLineRegex == null || !mExcludedLineRegex.IsMatch(line));

                string snippetFinalBody = string.Join(Environment.NewLine, unindentedAndIncludedLines);

                snippets.Add(new Snippet(source, snippetName, snippetFinalBody));
            }

            return snippets;
        }

        public void SubstituteFileSnippets([NotNull] string filePath, [NotNull] Dictionary<string, Snippet> ersatzSnippets)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");
            if (ersatzSnippets == null) throw new ArgumentNullException("ersatzSnippets");

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

        public string SubstituteSnippets([NotNull] string txtContainingSnippets,
                                         [NotNull] Dictionary<string, Snippet> ersatzSnippets)
        {
            if (txtContainingSnippets == null) throw new ArgumentNullException("txtContainingSnippets");
            if (ersatzSnippets == null) throw new ArgumentNullException("ersatzSnippets");

            // ReSharper disable once AssignNullToNotNullAttribute
            return mSnippetRegex.Replace(txtContainingSnippets, match => ReplaceSnippet(ersatzSnippets, match));
        }

        private string ReplaceSnippet([NotNull, ItemNotNull] Dictionary<string, Snippet> ersatzSnippets, [NotNull] Match match)
        {
            if (ersatzSnippets == null) throw new ArgumentNullException("ersatzSnippets");
            if (match == null) throw new ArgumentNullException("match");

            string snippetName;
            string snippetBody;
            GetSnippetNameAndBody(match, out snippetName, out snippetBody);

            Snippet ersatzSnippet;
            if (!ersatzSnippets.TryGetValue(snippetName, out ersatzSnippet))
            {
                string msg = string.Format("Snippet '{0}' not found in ersatzSnippets. List of available Snippets: {1}",
                                           snippetName,
                                           string.Join(", ", ersatzSnippets.Keys));
                throw new ArgumentException(msg);
            }

            // ReSharper disable once PossibleNullReferenceException
            string trimmedSnippet = ersatzSnippet.Body.TrimEnd();
            if (snippetBody != trimmedSnippet)
                Console.WriteLine("Snippet '{0}' changed... upgrading it", snippetName);
            else
                Console.WriteLine("Snippet '{0}' didn't change", snippetName);

            return trimmedSnippet;
        }

        private static void GetSnippetNameAndBody(Match match, [NotNull] out string snippetName, [NotNull] out string snippetBody)
        {
            // ReSharper disable once PossibleNullReferenceException
            GroupCollection groups = match.Groups;

            Group groupForName = groups["name"];
            if (groupForName == null)
                throw new ArgumentException("No Group found for 'name'");

            Group groupForBody = groups["body"];
            if (groupForBody == null)
                throw new ArgumentException("No Group found for 'body'");

            snippetName = groupForName.Captures[0].Value.Trim();
            snippetBody = groupForBody.Captures[0].Value;
        }
    }
}