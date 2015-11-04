using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NUtil.Doc
{
    public class ConsoleRecorder
    {
        private readonly List<Snippet> mSnippets = new List<Snippet>();
        private ConsoleRecord mConsoleRecord;

        public IEnumerable<Snippet> Snippets
        {
            get { return mSnippets; }
        }

        public void BeginRecord(string snippetName, [CanBeNull] Func<string, string> postProcessing = null)
        {
            mConsoleRecord = new ConsoleRecord(snippetName, postProcessing);
        }

        public void EndRecord()
        {
            mConsoleRecord.Stop();
            mSnippets.Add(new Snippet("Console", mConsoleRecord.RecordName, mConsoleRecord.ConsoleOutput));
        }
    }
}