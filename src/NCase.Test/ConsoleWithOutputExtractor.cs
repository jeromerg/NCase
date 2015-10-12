using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace NCase.Test
{
    public class ConsoleWithOutputExtractor
    {
        private static readonly Dictionary<string, int> sLastUsedIndexbyFilePathMember = new Dictionary<string, int>();

        public static void WriteLine(string txt, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            bool isNewBlock = txt.StartsWith("#");
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

            Directory.CreateDirectory(Path.GetDirectoryName(outputFileName));
            
            using (StreamWriter sw = File.AppendText(outputFileName))
            {
                sw.Write(txt);            
                Console.Write(txt);
            }
        }

        private static string GetOutputFilename(string callerFilePath, string memberName, int blockIndex)
        {
            string outputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);

            // Ensures directory
            string filename = string.Format(@"Extracts\{0}\Output_{1}_{2}.txt", outputFileNameWithoutExtension, memberName, blockIndex);

            return filename;
        }

        private static string GetFilePathMember(string filePath, string memberName)
        {
            return filePath + @"\" + memberName;
        }
    }
}