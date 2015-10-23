﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NCaseFramework.doc
{
    public class ConsoleRecorder
    {
        private readonly List<ConsoleRecord> mRecords = new List<ConsoleRecord>();
  
        public void WriteLine(string txt, [CallerMemberName] string callerMemberName = null)
        {
            Write(txt + Environment.NewLine, callerMemberName);
        }

        public void Write(string txt, 
                          [CallerFilePath] string callerFilePath = null, 
                          [CallerMemberName] string callerMemberName = null,
                          [CallerLineNumber] int callerLineNumber = -1)
        {
            mRecords.Add(new ConsoleRecord(txt, callerMemberName, callerFilePath, callerLineNumber));
        }

        public IReadOnlyList<ConsoleRecord> Records
        {
            get { return mRecords; }
        }
    }
}