using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class FileAnalyzer : IFileAnalyzer
    {

        [NotNull] private readonly ConcurrentDictionary<string, int[]> mIndentationCache = new ConcurrentDictionary<string, int[]>();

        [NotNull]
        private readonly IFileCache mFileCache;

        public FileAnalyzer([NotNull] IFileCache fileCache)
        {
            mFileCache = fileCache;
        }

        public bool IsEmptyLine([NotNull] string fileName, int lineIndex)
        {
            string line = mFileCache.GetLine(fileName, lineIndex);
            return string.IsNullOrWhiteSpace(line);
        }

        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public int GetIndentation([NotNull] string fileName, int lineIndex)
        {
            int[] fileIndentations = mIndentationCache.GetOrAdd(fileName,
                fn =>
                {
                    string[] lines = mFileCache.GetFileLines(fn);
                    return lines
                        .Select(s => s.TakeWhile(c => c == '\t' || c == ' ').Count())
                        .ToArray();
                });

            return fileIndentations[lineIndex - 1];
        }
    }
}