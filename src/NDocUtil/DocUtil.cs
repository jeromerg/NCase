using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ColorCode;
using JetBrains.Annotations;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace NDocUtil
{
    public class DocUtil
    {
        private const string SNIPPET_REGEX_STRING_ARG0_MARKER =
            @"(?<=^\s*{0}\s+(?<name>\w+)\W*?\r\n)(?<body>.*?)(?=\r\n[ \t]*(?:{0}|\Z))";

        private const string MARKDOWN_SNIPPET_REGEX_STRING =
            @"(?<=^\s*<!--#\s+(?<name>\w+)\s*-->[^\r\n]*\r\n```[^\r\n]*\r\n)(?<body>.*?)(?=\r\n[ \t]*```)";

        private const string DOC_FILE_EXTENSION = ".md";
        private const string SNIPPET_RAW_FILE_EXTENSION = ".snippet";
        private const string SNIPPET_HTML_TEMPLATE = "<html><body>\n{0}\n</html></body>";

        private const string CODE_SNIPPET_MARKER = @"//#";

        [NotNull] private readonly CodeColorizer mCodeColorizer = new CodeColorizer();
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

        public void UpdateDocAssociatedToThisFile([NotNull, CallerFilePath] string callerFilePath = "")
        {
            List<Snippet> allSnippets = GetAllSnippets(callerFilePath);

            // ReSharper disable PossibleNullReferenceException
            Dictionary<string, Snippet> allSnippetDictionary = allSnippets.ToDictionary(s => s.Name);
            // ReSharper restore PossibleNullReferenceException

            string docPath = GetAssociatedDocPath(callerFilePath);
            mMarkdownSnippetParser.SubstituteFileSnippets(docPath, allSnippetDictionary);
        }

        public void SaveSnippets([NotNull, CallerFilePath] string callerFilePath = "")
        {
            // ReSharper disable once PossibleNullReferenceException
            SaveSnippetsImp(callerFilePath, snippet =>
                {
                    SaveSnippet(callerFilePath, snippet.Name, snippet.Body, SNIPPET_RAW_FILE_EXTENSION);                     
                });
        }

        public void SaveSnippetsAsImage([NotNull] ImageFormat imageFormat, [NotNull, CallerFilePath] string callerFilePath = "")
        {
            // ReSharper disable once PossibleNullReferenceException
            SaveSnippetsImp(callerFilePath, snippet =>
                {
                    ILanguage language = snippet.Source == ConsoleRecorder.CONSOLE_SOURCE_NAME
                                    ? Languages.PowerShell
                                    : Languages.CSharp;

                    string divSnippet = mCodeColorizer.Colorize(snippet.Body, language);
                    string htmlSnippet = string.Format(SNIPPET_HTML_TEMPLATE, divSnippet);
                    string snippetFilePath = BuildSnippetFilePath(callerFilePath, snippet.Name, "." + imageFormat.ToString().ToLower());

                    
                    Image image = HtmlRender.RenderToImage(htmlSnippet);
                    image.Save(snippetFilePath, imageFormat);

                });
        }

        private void SaveSnippetsImp([NotNull] string callerFilePath, [NotNull] Action<Snippet> saveAction)
        {
            List<Snippet> allSnippets = GetAllSnippets(callerFilePath);

            Console.WriteLine("-- Now saving snippet as file --");

            foreach (Snippet snippet in allSnippets)
                saveAction(snippet);
        }

        private static void SaveSnippet([NotNull] string callerFilePath,
                                        [NotNull] string snippetName,
                                        [NotNull] string snippetBody,
                                        [NotNull] string snippetFileExtension)
        {
            string snippetFilePath = BuildSnippetFilePath(callerFilePath, snippetName, snippetFileExtension);

            Console.WriteLine("Writing snippet {0}", snippetFilePath);

            File.WriteAllText(snippetFilePath, snippetBody);

            Console.WriteLine("Written snippet {0}", snippetFilePath);
        }

        [NotNull]
        private List<Snippet> GetAllSnippets([NotNull] string callerFilePath)
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
            return allSnippets;
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

        [NotNull]
        private static string BuildSnippetFilePath(
            [NotNull] string callerFilePath,
            [NotNull] string fileNameWithoutExtension,
            [NotNull] string fileExtension)
        {
            string docPath = string.Format("{0}{1}{2}{3}",
                                           Path.GetDirectoryName(callerFilePath),
                                           Path.DirectorySeparatorChar,
                                           fileNameWithoutExtension,
                                           fileExtension);
            return docPath;
        }
    }
}