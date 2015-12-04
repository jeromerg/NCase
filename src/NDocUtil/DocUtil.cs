using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ColorCode;
using JetBrains.Annotations;
using NDocUtil.Console;
using NDocUtil.ExportToImage;
using NDocUtil.Snippets;

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
        
        [NotNull] private readonly string mDemoPathSubstitute;
        [NotNull] private readonly string mFilePath;
        [NotNull] private readonly string mFileNameWithoutExtension;
        [NotNull] private readonly string mFileDir;
        
        [NotNull] private readonly SnippetParser mMarkdownSnippetParser;
        [NotNull] private readonly SnippetParser mCodeSnippetParser;
        [NotNull] private readonly ConsoleRecorder mConsoleRecorder = new ConsoleRecorder();

        public DocUtil([NotNull] string codeExcludedLineRegexString, [NotNull] string demoPathSubstitute, [NotNull, CallerFilePath] string filePath = "")
        {
            if (codeExcludedLineRegexString == null) throw new ArgumentNullException("codeExcludedLineRegexString");
            if (demoPathSubstitute == null) throw new ArgumentNullException("demoPathSubstitute");

            mDemoPathSubstitute = demoPathSubstitute;
            mFilePath = filePath;
            string callerFileDir = Path.GetDirectoryName(filePath);
            if (callerFileDir == null) throw new ArgumentException("callerFileDir is null");
            mFileDir = callerFileDir;
            string callerFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            if (callerFileNameWithoutExtension == null) throw new ArgumentException("callerFileNameWithoutExtension is null");
            mFileNameWithoutExtension = callerFileNameWithoutExtension;

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

        public void UpdateDocAssociatedToThisFile()
        {
            List<Snippet> allSnippets = GetAllSnippets();

            // ReSharper disable PossibleNullReferenceException
            Dictionary<string, Snippet> allSnippetDictionary = allSnippets.ToDictionary(s => s.Name);
            // ReSharper restore PossibleNullReferenceException

            string docPath = BuildAssociatedDocFilePath();
            mMarkdownSnippetParser.SubstituteFileSnippets(docPath, allSnippetDictionary);
        }

        public void SaveSnippets()
        {
            foreach (Snippet sn in GetAllSnippets())
            {
                string path = BuildSnippetFilePath(sn.Name, SNIPPET_RAW_FILE_EXTENSION);
                
                LogSaving(path);
                
                File.WriteAllText(path, sn.Body);
                
                LogSaved(path);
            }
        }

        public void SaveSnippetsAsImage([NotNull] ImageFormat imageFormat)
        {
            foreach (Snippet sn in GetAllSnippets())
            {
                string ext = "." + imageFormat.ToString().ToLower();
                string path = BuildSnippetFilePath(sn.Name, ext);
                
                LogSaving(path);
            
                ILanguage language = sn.Source == ConsoleRecorder.CONSOLE_SOURCE_NAME
                                ? Languages.PowerShell
                                : Languages.CSharp;

                string divSnippet = mCodeColorizer.Colorize(sn.Body, language);
                string htmlSnippet = string.Format(SNIPPET_HTML_TEMPLATE, divSnippet);

                Metafile image = HtmlToWmfUtil.Convert(htmlSnippet);
                image.SaveAsEmf(path);
                
                LogSaved(path);
            }
        }

        [NotNull, ItemNotNull]
        private List<Snippet> GetAllSnippets()
        {
            // REPLACE ALL PATH TO THIS FOLDER TO THE mDemoPathSubstitute PATH
            IEnumerable<Snippet> consoleSnippets = mConsoleRecorder
                .Snippets
                .Select(s => new Snippet(s.Source,
                                         s.Name,
                                         s.Body.Replace(mFileDir, mDemoPathSubstitute)));

            var allSnippets = new List<Snippet>();
            allSnippets.AddRange(ExtractCodeSnippets(mFilePath));
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

        private void LogSaving(string path)
        {
            Console.WriteLine("- Saving {0}", path);
        }

        private void LogSaved(string path)
        {
            Console.WriteLine("- Saved {0}", path);
        }

        [NotNull]
        private string BuildAssociatedDocFilePath()
        {
            string docFilePath = string.Format("{0}{1}{2}{3}",
                                           mFileDir,
                                           Path.DirectorySeparatorChar,
                                           mFileNameWithoutExtension,
                                           DOC_FILE_EXTENSION);
            return docFilePath;
        }

        [NotNull]
        private string BuildSnippetFilePath(
            [NotNull] string fileNameWithoutExtension,
            [NotNull] string fileExtension)
        {
            string docPath = string.Format("{0}{1}{2}{3}",
                                           mFileDir,
                                           Path.DirectorySeparatorChar,
                                           fileNameWithoutExtension,
                                           fileExtension);
            return docPath;
        }
    }


}