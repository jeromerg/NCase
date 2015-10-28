using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NCaseFramework.doc;
using NUtil.File;

namespace NUtil.Snippet
{
    public static class DocUtil
    {
        public static void UpdateDocSnippets(
            [CanBeNull] ConsoleRecorder consoleRecorder = null,
            [NotNull] string docFileExtension = ".md", 
            [CallerFilePath] string callerFilePath = null,
            string codeSnippetMarker = @"//#",
            [CanBeNull] string codeExcludedLineRegex = "#",
            string consoleSnippetMarker = @"#",
            string documentSnippetMarker = @"<!--#")
        {
            Dictionary<string, string> allSnippets = new Dictionary<string, string>();

            Console.WriteLine("Parsing code snippets");
            string thisFileContent = System.IO.File.ReadAllText(callerFilePath);
            var codeSnippets = new SnippetParser(codeSnippetMarker, codeExcludedLineRegex);
            Dictionary<string, string> snippets = codeSnippets.ParseSnippets(thisFileContent);

            foreach (var snippetPair in snippets)
                allSnippets.Add(snippetPair.Key, snippetPair.Value);

            if (consoleRecorder == null)
            {
                Console.WriteLine("No console snippet to parse");
            }
            else
            {
                Console.WriteLine("Parsing console snippets");
                var consoleSnippets = new SnippetParser(consoleSnippetMarker);
                var recordsByMember = consoleRecorder
                    .Records
                    .GroupBy(r => new { r.CallerFilePath, r.CallerMemberName }, r => r.Text);

                foreach (var memberRecords in recordsByMember)
                {
                    string memberConsole = string.Join("", memberRecords);
                    Console.WriteLine("Parsing console record of method '{0}'", memberRecords.Key.CallerMemberName);
                    Dictionary<string, string> recordSnippets = consoleSnippets.ParseSnippets(memberConsole);
                    foreach (var recordSnippet in recordSnippets)
                        allSnippets.Add(recordSnippet.Key, recordSnippet.Value);
                }

            }

            string documentFilePath = Path.GetDirectoryName(callerFilePath)
                                  + Path.DirectorySeparatorChar
                                  + Path.GetFileNameWithoutExtension(callerFilePath)
                                  + docFileExtension;

            Console.WriteLine("Updating snippets within '{0}'", documentFilePath);
            string docFileContent = System.IO.File.ReadAllText(documentFilePath);
            var docSnippetParser = new SnippetParser(documentSnippetMarker);
            string updatedDocFileContent = docSnippetParser.SubstituteSnippets(docFileContent, allSnippets);

            if(updatedDocFileContent == docFileContent)
            {
                Console.WriteLine("Document didn't change. No update performed");
                return;
            }

            Console.WriteLine("Document changed. Saving new document file content");
            System.IO.File.Delete(documentFilePath); // TODO DELETE USING WINDOWS SAFE DELETE FUNCTION
            System.IO.File.WriteAllText(documentFilePath, updatedDocFileContent);
        }
    }
}