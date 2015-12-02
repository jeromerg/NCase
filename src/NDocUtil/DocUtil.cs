using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace NDocUtil
{
    public class DocUtil
    {
        private const string SNIPPET_REGEX_STRING_ARG0_MARKER =
            @"(?<=^\s*{0}\s+(?<name>\w+)\W*?\r\n)(?<body>.*?)(?=\r\n[ \t]*(?:{0}|\Z))";

        private const string MARKDOWN_SNIPPET_REGEX_STRING =
            @"(?<=^\s*<!--#\s+(?<name>\w+)\s*-->[^\r\n]*\r\n```[^\r\n]*\r\n)(?<body>.*?)(?=\r\n[ \t]*```)";

        private const string DOC_FILE_EXTENSION = ".md";
        private const string CODE_SNIPPET_MARKER = @"//#";

        [NotNull] private readonly string mDemoPath;
        [NotNull] private readonly SnippetParser mMarkdownSnippetParser;
        [NotNull] private readonly SnippetParser mCodeSnippetParser;
        [NotNull] private readonly ConsoleRecorder mConsoleRecorder = new ConsoleRecorder();

        public DocUtil([NotNull] string codeExcludedLineRegexString, [NotNull] string demoPath)
        {
            if (codeExcludedLineRegexString == null) throw new ArgumentNullException("codeExcludedLineRegexString");
            if (demoPath == null) throw new ArgumentNullException("demoPath");

            mDemoPath = demoPath;
            var markdownSnippetRegex = new Regex(MARKDOWN_SNIPPET_REGEX_STRING, RegexOptions.Singleline | RegexOptions.Multiline);
            mMarkdownSnippetParser = new SnippetParser(markdownSnippetRegex, null);

            string codeSnippetRegexString = string.Format(SNIPPET_REGEX_STRING_ARG0_MARKER, CODE_SNIPPET_MARKER);
            var codeSnippetRegex = new Regex(codeSnippetRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            var codeExcludedLineRegex = new Regex(codeExcludedLineRegexString);
            mCodeSnippetParser = new SnippetParser(codeSnippetRegex, codeExcludedLineRegex);
        }

        public void BeginRecordConsole([NotNull] string snippetName, [CanBeNull] Func<string, string> postProcessing = null)
        {
            if (snippetName == null) throw new ArgumentNullException("snippetName");
            mConsoleRecorder.BeginRecord(snippetName, postProcessing);
        }

        public void StopRecordConsole()
        {
            mConsoleRecorder.EndRecord();
        }

        public void UpdateDocAssociatedToThisFile([CallerFilePath] string callerFilePath = null)
        {
            string callerFileDir = Path.GetDirectoryName(callerFilePath);
            if (callerFileDir == null) throw new ArgumentException("callerFileDir is null");

            // REPLACE ALL PATH TO THIS FOLDER TO THE mDemoPath PATH
            IEnumerable<Snippet> consoleSnippets = mConsoleRecorder
                .Snippets
                .Select(s => new Snippet(s.Source,
                                         s.Name,
                                         s.Body.Replace(callerFileDir, mDemoPath)));

            var allSnippets = new List<Snippet>();
            allSnippets.AddRange(ExtractCodeSnippets(callerFilePath));
            allSnippets.AddRange(consoleSnippets);

            Console.WriteLine("All available snippets:");

            // ReSharper disable PossibleNullReferenceException
            foreach (Snippet s in allSnippets)
                Console.WriteLine("Snippet '{0}', Body Length {1}, Source '{2}'", s.Name, s.Body.Length, s.Source);
            // ReSharper restore PossibleNullReferenceException

            // ReSharper disable PossibleNullReferenceException
            Dictionary<string, Snippet> allSnippetDictionary = allSnippets.ToDictionary(s => s.Name);
            // ReSharper restore PossibleNullReferenceException

            string docPath = GetAssociatedDocPath(callerFilePath);
            mMarkdownSnippetParser.SubstituteFileSnippets(docPath, allSnippetDictionary);
        }

        [NotNull]
        private List<Snippet> ExtractCodeSnippets([NotNull] string callerFilePath)
        {
            return mCodeSnippetParser.ParseFileSnippets(callerFilePath);
        }

        [NotNull]
        private static string GetAssociatedDocPath([NotNull] string callerFilePath)
        {
            string docPath = string.Format("{0}{1}{2}{3}",
                                           Path.GetDirectoryName(callerFilePath),
                                           Path.DirectorySeparatorChar,
                                           Path.GetFileNameWithoutExtension(callerFilePath),
                                           DOC_FILE_EXTENSION);
            return docPath;
        }
    }
}