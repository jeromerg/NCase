using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ColorCode;
using JetBrains.Annotations;
using NDocUtilLibrary.ConsoleUtil;
using NDocUtilLibrary.ExportToImage;
using NDocUtilLibrary.Snippets;

namespace NDocUtilLibrary
{
    public class NDocUtil
    {
        private const string SNIPPET_REGEX_STRING_ARG0_MARKER =
            @"(?<=^\s*//#\s+(?<name>\w+)\W*?\r\n)(?<body>.*?[^\r\n])(?=\r\n[ \t]*(?://#|\Z))";

        private const string MARKDOWN_SNIPPET_REGEX_STRING =
            @"(?<=^(?<indent>[ \t]*)<!--#[ \t]+(?<name>\w+)[ \t]*-->[ \t]*\r\n[ \t]*```[^\r\n]*\r\n)(?<body>.*?)(?=\r\n[ \t]*```)";

        private const string DOC_FILE_EXTENSION = ".md";
        private const string SNIPPET_HTML_TEMPLATE = "<html><body>\n{0}\n</html></body>";

        private const string CODE_SNIPPET_MARKER = @"//#";

        [NotNull] private readonly CodeColorizer mCodeColorizer = new CodeColorizer();

        [NotNull] private readonly string mFilePath;
        [NotNull] private readonly string mFileNameWithoutExtension;
        [NotNull] private readonly string mFileDir;

        [NotNull] private readonly SnippetParser mMarkdownSnippetParser;
        [NotNull] private readonly SnippetParser mCodeSnippetParser;
        [NotNull] private readonly ConsoleRecorder mConsoleRecorder = new ConsoleRecorder();

        public NDocUtil([CanBeNull] string codeExcludedLineRegexString = null,
                        int tabIndentation = 4,
                        [NotNull, CallerFilePath] string filePath = "")
        {
            mFilePath = filePath;
            string callerFileDir = Path.GetDirectoryName(filePath);
            if (callerFileDir == null) throw new ArgumentException("callerFileDir is null");
            mFileDir = callerFileDir;
            string callerFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            if (callerFileNameWithoutExtension == null) throw new ArgumentException("callerFileNameWithoutExtension is null");
            mFileNameWithoutExtension = callerFileNameWithoutExtension;

            var markdownSnippetRegex = new Regex(MARKDOWN_SNIPPET_REGEX_STRING, RegexOptions.Singleline | RegexOptions.Multiline);
            mMarkdownSnippetParser = new SnippetParser(markdownSnippetRegex, null, tabIndentation);

            string codeSnippetRegexString = string.Format(SNIPPET_REGEX_STRING_ARG0_MARKER, CODE_SNIPPET_MARKER);
            var codeSnippetRegex = new Regex(codeSnippetRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            Regex codeExcludedLineRegex = codeExcludedLineRegexString != null
                                              ? new Regex(codeExcludedLineRegexString)
                                              : null;
            mCodeSnippetParser = new SnippetParser(codeSnippetRegex, codeExcludedLineRegex, tabIndentation);
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

        public void SaveSnippetsAsRaw([NotNull] string path = @"snippet\", [NotNull] string fileExtension = ".snippet")
        {
            if (fileExtension == null) throw new ArgumentNullException("fileExtension");

            foreach (Snippet sn in GetAllSnippets())
            {
                string outputPath = BuildSnippetFilePathAndEnsureItExists(path, sn.Name, fileExtension);

                LogSaving(outputPath);

                File.WriteAllText(outputPath, sn.Body);

                LogSaved(outputPath);
            }
        }

        public void SaveSnippetsAsHtml([NotNull] string htmlSnippetDecorator = "{0}",
                                       [NotNull] string path = @"snippet\",
                                       [NotNull] string fileExtension = ".html")
        {
            if (htmlSnippetDecorator == null) throw new ArgumentNullException("htmlSnippetDecorator");
            if (fileExtension == null) throw new ArgumentNullException("fileExtension");

            foreach (Snippet sn in GetAllSnippets())
            {
                string outputPath = BuildSnippetFilePathAndEnsureItExists(path, sn.Name, fileExtension);

                LogSaving(outputPath);

                ILanguage language = sn.Source == ConsoleRecorder.CONSOLE_SOURCE_NAME
                                         ? Languages.PowerShell
                                         : Languages.CSharp;

                string divSnippet = mCodeColorizer.Colorize(sn.Body, language);
                string htmlSnippet = string.Format(htmlSnippetDecorator, divSnippet);

                File.WriteAllText(outputPath, htmlSnippet);

                LogSaved(outputPath);
            }
        }

        public void SaveSnippetsAsImage(
            [NotNull] ImageFormat imageFormat,
            [NotNull] string path = @"snippet\",
            [CanBeNull] string fileExtension = null,
            float leftBorder = 0,
            float topBorder = 0,
            float rightBorder = 0,
            float bottomBorder = 0)
        {
            foreach (Snippet sn in GetAllSnippets())
            {
                string ext = fileExtension ?? ("." + imageFormat.ToString().ToLower());
                string outputPath = BuildSnippetFilePathAndEnsureItExists(path, sn.Name, ext);

                LogSaving(outputPath);

                ILanguage language = sn.Source == ConsoleRecorder.CONSOLE_SOURCE_NAME
                                         ? Languages.PowerShell
                                         : Languages.CSharp;

                string divSnippet = mCodeColorizer.Colorize(sn.Body, language);
                string htmlSnippet = string.Format(SNIPPET_HTML_TEMPLATE, divSnippet);

                Metafile image = HtmlToMetafileUtil.Convert(htmlSnippet, leftBorder, topBorder, rightBorder, bottomBorder);

                switch (imageFormat)
                {
                    case ImageFormat.Bmp:
                        image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case ImageFormat.Png:
                        image.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case ImageFormat.Emf:
                        image.SaveAsVectorEmf(outputPath);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("imageFormat", imageFormat, null);
                }

                LogSaved(outputPath);
            }
        }

        [NotNull, ItemNotNull]
        private List<Snippet> GetAllSnippets()
        {
            var allSnippets = new List<Snippet>();
            allSnippets.AddRange(ExtractCodeSnippets(mFilePath));
            allSnippets.AddRange(mConsoleRecorder.Snippets);

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
        private string BuildSnippetFilePathAndEnsureItExists([NotNull] string path,
                                                             [NotNull] string fileNameWithoutExtension,
                                                             [NotNull] string fileExtension)
        {
            string fullPath = Path.IsPathRooted(path)
                                  ? path
                                  : Path.Combine(mFileDir, path);

            Directory.CreateDirectory(fullPath);

            string docPath = string.Format("{0}{1}{2}{3}",
                                           fullPath,
                                           Path.DirectorySeparatorChar,
                                           fileNameWithoutExtension,
                                           fileExtension);
            return docPath;
        }
    }
}