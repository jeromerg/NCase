using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace NUtil.Doc
{
    public class DocUtil
    {
        private const string SNIPPET_REGEX_STRING_ARG0_MARKER =
            @"(?<=^\s*{0}\s+(?<name>\w+)\W*?\r\n)(?<body>.*?)(?=\r\n[ \t]*(?:{0}|\Z))";

        private const string MARKDOWN_SNIPPET_REGEX_STRING =
            @"(?<=^\s*<!--#\s+(?<name>\w+)\s*-->[^\r\n]*\r\n```[^\r\n]*\r\n)(?<body>.*?)(?=\r\n\s*```)";

        private const string DOC_FILE_EXTENSION = ".md";
        private const string CODE_SNIPPET_MARKER = @"//#";

        private readonly SnippetParser mMarkdownSnippetParser;
        private readonly SnippetParser mCodeSnippetParser;

        private readonly ConsoleRecorder mConsoleRecorder = new ConsoleRecorder();

        public DocUtil(string codeExcludedLineRegexString)
        {
            var markdownSnippetRegex = new Regex(MARKDOWN_SNIPPET_REGEX_STRING, RegexOptions.Singleline | RegexOptions.Multiline);
            mMarkdownSnippetParser = new SnippetParser(markdownSnippetRegex, null);

            string codeSnippetRegexString = string.Format(SNIPPET_REGEX_STRING_ARG0_MARKER, CODE_SNIPPET_MARKER);
            var codeSnippetRegex = new Regex(codeSnippetRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            var codeExcludedLineRegex = new Regex(codeExcludedLineRegexString);
            mCodeSnippetParser = new SnippetParser(codeSnippetRegex, codeExcludedLineRegex);
        }

        public void BeginRecordConsole(string snippetName)
        {
            mConsoleRecorder.BeginRecord(snippetName);
        }

        public void StopRecordConsole()
        {
            mConsoleRecorder.EndRecord();
        }

        public void UpdateDocAssociatedToThisFile([CallerFilePath] string callerFilePath = null)
        {
            var allSnippets = new List<Snippet>();
            allSnippets.AddRange(ExtractCodeSnippets(callerFilePath));
            allSnippets.AddRange(mConsoleRecorder.Snippets);

            Console.WriteLine("All available snippets:");
            foreach (Snippet s in allSnippets)
                Console.WriteLine("Snippet '{0}', Body Length {1}, Source '{2}'", s.Name, s.Body.Length, s.Source);

            Dictionary<string, Snippet> allSnippetDictionary = allSnippets.ToDictionary(s => s.Name);

            string docPath = GetAssociatedDocPath(callerFilePath);
            mMarkdownSnippetParser.SubstituteFileSnippets(docPath, allSnippetDictionary);
        }

        private List<Snippet> ExtractCodeSnippets(string callerFilePath)
        {
            return mCodeSnippetParser.ParseFileSnippets(callerFilePath);
        }

        private static string GetAssociatedDocPath(string callerFilePath)
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