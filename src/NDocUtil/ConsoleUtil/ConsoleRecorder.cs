using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDocUtil.Snippets;

namespace NDocUtil.ConsoleUtil
{
    public class ConsoleRecorder
    {
        public const string CONSOLE_SOURCE_NAME = "Console";

        [NotNull, ItemNotNull] private readonly List<Snippet> mSnippets = new List<Snippet>();
        [CanBeNull] private ConsoleRecord mConsoleRecord;

        [NotNull, ItemNotNull] public IEnumerable<Snippet> Snippets
        {
            get { return mSnippets; }
        }

        public void BeginRecord([NotNull] string snippetName, [CanBeNull] Func<string, string> postProcessing = null)
        {
            mConsoleRecord = new ConsoleRecord(snippetName, postProcessing);
        }

        public void EndRecord()
        {
            if (mConsoleRecord == null)
                throw new InvalidOperationException("mConsoleRecord is null: BeginRecord was not called before");

            mConsoleRecord.Stop();
            mSnippets.Add(new Snippet(CONSOLE_SOURCE_NAME, mConsoleRecord.RecordName, mConsoleRecord.ConsoleOutput));
        }
    }
}