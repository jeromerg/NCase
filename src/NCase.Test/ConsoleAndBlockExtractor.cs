using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NCaseFramework.Test
{
    public class ConsoleAndBlockExtractor
    {
        private const string CONSOLE_BLOCK_BEGIN = "#";
        private const string FILE_BLOCK_REGEX = @"(\r\n|^)[\t ]*//[\t ]*BeginExtract\s+(?<name>[^\r\n]+)\r\n(?<body>.*)\r\n[\t ]*//[\t ]*EndExtract";
        private const string EXTRACT_DIR = "Extracts";
        private static readonly Regex sFileBlockRegex = new Regex(FILE_BLOCK_REGEX,  RegexOptions.Singleline | RegexOptions.Multiline);

        private static readonly Dictionary<string, int> sLastUsedIndexbyFilePathMember = new Dictionary<string, int>();
        
        public void DumpAllExtractOfThisFile([CallerFilePath] string callerFilePath = null)
        {
            string fileTxt = File.ReadAllText(callerFilePath);
            MatchCollection blocks = sFileBlockRegex.Matches(fileTxt);

            foreach (Match block in blocks)
            {
                string blockName = block.Groups["name"].Captures[0].Value;
                string blockBody = block.Groups["body"].Captures[0].Value;

                string unindentedBody = Desindent(blockBody);

                string outputFileName = GetBlockFilename(callerFilePath, blockName);
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                // ReSharper disable once AssignNullToNotNullAttribute
                Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));

                File.WriteAllText(outputFileName, unindentedBody);
            }
        }

        public static void WriteLine(string txt, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            bool isNewBlock = txt.StartsWith(CONSOLE_BLOCK_BEGIN);
            string filePathMember = GetFilePathMember(callerFilePath, callerMemberName);

            // INCREMENT / GET CURRENT INDEX
            int currentIndex;
            if (!sLastUsedIndexbyFilePathMember.TryGetValue(filePathMember, out currentIndex))
            {
                if (!isNewBlock)
                    return; // NO DUMP BEFORE FIRST # CHARACTER

                currentIndex = 0;
            }
            else
            {
                currentIndex += isNewBlock ? 1 : 0;
            }
            sLastUsedIndexbyFilePathMember[filePathMember] = currentIndex;

            // IF NEW BLOCK, THEN DELETE OLD DUMP
            if (isNewBlock)
            {
                string outputFileName = GetOutputFilename(callerFilePath, callerMemberName, currentIndex);
                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

            }

            Write(txt, callerFilePath, callerMemberName, currentIndex);
            Write(Environment.NewLine, callerFilePath, callerMemberName, currentIndex);
        }

        public static void Write(string txt, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string filePathMember = GetFilePathMember(callerFilePath, callerMemberName);
            Write(txt, callerFilePath, callerMemberName, sLastUsedIndexbyFilePathMember[filePathMember]);
        }

        private static void Write(string txt, string callerFilePath, string callerMemberName, int blockIndex)
        {
            string outputFileName = GetOutputFilename(callerFilePath, callerMemberName, blockIndex);

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            
            using (StreamWriter sw = File.AppendText(outputFileName))
            {
                sw.Write(txt);            
                Console.Write(txt);
            }
        }

        private static string GetBlockFilename(string callerFilePath, string blockName)
        {
            string outputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);

            // Ensures directory
            string filename = string.Format(@"{0}\{1}\Block_{2}.txt", EXTRACT_DIR, outputFileNameWithoutExtension, blockName);

            return filename;
        }

        private static string GetOutputFilename(string callerFilePath, string memberName, int blockIndex)
        {
            string outputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);

            // Ensures directory
            string filename = string.Format(@"{0}\{1}\Output_{2}_{3}.txt", EXTRACT_DIR, outputFileNameWithoutExtension, memberName, blockIndex);

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
            foreach (var line in lines)
            {
                sb.AppendLine(line.Length >= indentMin ? line.Substring(indentMin) : "");
            }
            return sb.ToString();
        }

        private static int GetIndent(string s)
        {
            int result = 0;
            foreach (char c in s) {
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