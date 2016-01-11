using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class FileCache : IFileCache
    {
        // currently most basic implementation
        [NotNull] private readonly ConcurrentDictionary<string, string[]> mFiles = new ConcurrentDictionary<string, string[]>();
        [NotNull] private readonly ConcurrentDictionary<string, int[]> mIndentations = new ConcurrentDictionary<string, int[]>();

        public string GetLine([NotNull] string fileName, int lineIndex)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string[] lines = GetFileLines(fileName);

            // ReSharper disable once PossibleNullReferenceException
            return lines[lineIndex];
        }

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public int GetIndentation([NotNull] string fileName, int lineIndex)
        {
            int[] fileIndentations = mIndentations.GetOrAdd(fileName,
                fn =>
                {
                    string[] lines = GetFileLines(fn);
                    return lines
                        .Select(s => s.TakeWhile(c => c == '\t' || c == ' ').Count())
                        .ToArray();
                });

            return fileIndentations[lineIndex - 1];
        }

        private string[] GetFileLines([NotNull] string fileName)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string[] lines = mFiles.GetOrAdd(fileName, fn => File.ReadAllLines(fn));
            return lines;
        }
    }
}