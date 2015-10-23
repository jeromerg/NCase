using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NCaseFramework.doc
{
    public class SourceAndConsoleSnippets
    {
        private readonly string mOutputDirRelatedToCallerFileDir;
        private readonly string mConsoleBlockBegin;
        private readonly Regex mFileBlockRegex;
        private readonly Regex mMarkdownFileInjectionBlockRegex;

        private readonly Dictionary<string, int> mLastUsedIndexByFilePathMember = new Dictionary<string, int>();

        public SourceAndConsoleSnippets()
            : this(outputDirRelatedToCallerFileDir: "Generated",
                   consoleBlockBegin: "#",
                   fileBlockRegexString: @"(\r\n|^)[\t ]*//#\s+(?<name>[^\r\n]+)\r\n(?<body>.*?)\r\n[\t ]*//#",
                   markdownInjectionRegexString: @"(?<=(<!--#\s+(?<name>[^-]+)-->\s*\r\n))(?<body>.*?(\r\n)?)(?=([\t ]*<!--#-->))"
                )
        {
        }

        public SourceAndConsoleSnippets(string outputDirRelatedToCallerFileDir,
                                        string consoleBlockBegin,
                                        string fileBlockRegexString,
                                        string markdownInjectionRegexString)
        {
            mOutputDirRelatedToCallerFileDir = outputDirRelatedToCallerFileDir;
            mConsoleBlockBegin = consoleBlockBegin;
            mFileBlockRegex = new Regex(fileBlockRegexString, RegexOptions.Multiline | RegexOptions.Singleline);
            mMarkdownFileInjectionBlockRegex = new Regex(markdownInjectionRegexString,
                                                         RegexOptions.Multiline | RegexOptions.Singleline);
        }

        public void Write(string txt,
                          [CallerFilePath] string callerFilePath = null,
                          [CallerMemberName] string callerMemberName = null)
        {
            string filePathMember = GetFilePathMember(callerFilePath, callerMemberName);
            Write(txt, callerFilePath, callerMemberName, mLastUsedIndexByFilePathMember[filePathMember]);
        }

        public void WriteLine(string txt,
                              [CallerFilePath] string callerFilePath = null,
                              [CallerMemberName] string callerMemberName = null)
        {
            bool isNewBlock = txt.StartsWith(mConsoleBlockBegin);
            string filePathMember = GetFilePathMember(callerFilePath, callerMemberName);

            // INCREMENT / GET CURRENT INDEX
            int currentIndex;
            if (!mLastUsedIndexByFilePathMember.TryGetValue(filePathMember, out currentIndex))
            {
                if (!isNewBlock)
                    return; // NO DUMP BEFORE FIRST # CHARACTER

                currentIndex = 1;
            }
            else
            {
                currentIndex += isNewBlock ? 1 : 0;
            }
            mLastUsedIndexByFilePathMember[filePathMember] = currentIndex;

            // IF NEW BLOCK, THEN DELETE OLD DUMP
            if (isNewBlock)
            {
                string outputFileName = BuildSnippetFileName(callerFilePath, callerMemberName, currentIndex);
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);
            }

            Write(txt, callerFilePath, callerMemberName, currentIndex);
            Write(Environment.NewLine, callerFilePath, callerMemberName, currentIndex);
        }

        public void ExportAllSnippetsOfThisFile([CallerFilePath] string callerFilePath = null)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string fileTxt = File.ReadAllText(callerFilePath);
            MatchCollection blocks = mFileBlockRegex.Matches(fileTxt);

            if (blocks.Count == 0)
            {
                Console.WriteLine("No snippet to export!");
                return;
            }

            Console.WriteLine("Found {0} snippets", blocks.Count);
            foreach (Match block in blocks)
            {
                string blockName = block.Groups["name"].Captures[0].Value.Trim();
                string blockBody = block.Groups["body"].Captures[0].Value;

                Console.WriteLine("Exporting snippets: '{0}'", blockName);

                string unindentedBody = Desindent(blockBody);

                string outputFileName = BuildSnippetFileName(callerFilePath, blockName);
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                // ReSharper disable once AssignNullToNotNullAttribute
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));

                File.WriteAllText(outputFileName, unindentedBody);

                Console.WriteLine("Exported snippets: '{0}' to {1}", blockName, outputFileName);
            }
        }

        public void InflateMarkdownFile([CallerFilePath] string callerFilePath = null)
        {
            string callerFileDir = Path.GetDirectoryName(callerFilePath);
            string callerFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);

            string markdownFile = Path.Combine(callerFileDir, callerFileNameWithoutExtension + ".md");

            if (!File.Exists(markdownFile))
                throw new ArgumentException(string.Format("file {0} not found", markdownFile));

            string inputMarkdownContent = File.ReadAllText(markdownFile);


            string outputMarkdownContent = mMarkdownFileInjectionBlockRegex.Replace(inputMarkdownContent,
                                                                      match => ReplaceMarkdownMatchBySnippet(callerFilePath, match));

            if(inputMarkdownContent != outputMarkdownContent)
                File.WriteAllText(markdownFile, outputMarkdownContent);
        }

        private string ReplaceMarkdownMatchBySnippet(string callerFilePath, Match match)
        {
            string snippetName = match.Groups["name"].Captures[0].Value.Trim();

            string snippetFilePath = BuildSnippetFileName(callerFilePath, snippetName);
            if(!File.Exists(snippetFilePath))
                throw new ArgumentException(string.Format(@"Snippet '{0}': Could not find snippet file '{1}'", snippetName, snippetFilePath));

            string snippetContent = File.ReadAllText(snippetFilePath);
            return snippetContent;
        }

        private void Write(string txt, string callerFilePath, string callerMemberName, int blockIndex)
        {
            string snippetFileName = BuildSnippetFileName(callerFilePath, callerMemberName, blockIndex);

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(snippetFileName));

            using (StreamWriter sw = File.AppendText(snippetFileName))
            {
                sw.Write(txt);
                Console.Write(txt);
            }
        }

        private string BuildSnippetFileName(string callerFilePath, string memberName, int blockIndex)
        {
            return BuildSnippetFileName(callerFilePath, string.Format("{0}{1}", memberName, blockIndex));
        }

        private string BuildSnippetFileName(string callerFilePath, string snippetName)
        {
            string callerFileDir = Path.GetDirectoryName(callerFilePath);
            string callerFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);

            // Ensures directory
            string filename = string.Format(@"{0}\{1}\{2}\{3}.md",
                                            callerFileDir,
                                            mOutputDirRelatedToCallerFileDir,
                                            callerFileNameWithoutExtension,
                                            snippetName);

            return filename;
        }

        private static string GetFilePathMember(string filePath, string memberName)
        {
            return filePath + @"\" + memberName;
        }

        private static string Desindent(string txt)
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