using System;
using System.Collections.Generic;

namespace NUtil.Doc
{
    public class ConsoleRecorder
    {
        private readonly List<Snippet> mSnippets = new List<Snippet>(); 
        private string mCurrentConsoleSnippetName;
        private ConsoleMirroring mCurrentConsoleMirroring;
        
        public void BeginRecord(string snippetName)
        {
            mCurrentConsoleSnippetName = snippetName;
            mCurrentConsoleMirroring = new ConsoleMirroring();
            Console.SetOut(mCurrentConsoleMirroring);
        }

        public void EndRecord()
        {
            mCurrentConsoleMirroring.Flush();
            
            mSnippets.Add(new Snippet("Console", mCurrentConsoleSnippetName, mCurrentConsoleMirroring.ToString()));
            mCurrentConsoleSnippetName = null;
            mCurrentConsoleMirroring = null;
        }

        public IEnumerable<Snippet> Snippets
        {
            get { return mSnippets; }
        }
    }
}