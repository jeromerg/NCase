using System.Collections.Concurrent;
using System.IO;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class FileCache : IFileCache
    {
        // currently most basic implementation
        [NotNull] private readonly ConcurrentDictionary<string, string[]> mFiles = new ConcurrentDictionary<string, string[]>();

        public string GetLine(string fileName, int lineIndex)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string[] lines = GetFileLines(fileName);

            // ReSharper disable once PossibleNullReferenceException
            string line = lines[lineIndex - 1];
            return line;
        }

        public string[] GetFileLines([NotNull] string fileName)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            string[] lines = mFiles.GetOrAdd(fileName, fn => File.ReadAllLines(fn));
            return lines;
        }
    }
}