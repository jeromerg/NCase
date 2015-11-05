using System;
using JetBrains.Annotations;

namespace NUtil.Doc
{
    public class ConsoleRecord
    {
        private readonly ConsoleMirroring mConsoleMirroring;
        private readonly string mRecordName;
        [CanBeNull] private readonly Func<string, string> mPostProcessing;

        public ConsoleRecord(string recordName, [CanBeNull] Func<string, string> postProcessing = null)
        {
            mConsoleMirroring = new ConsoleMirroring();
            mRecordName = recordName;
            mPostProcessing = postProcessing;
        }

        public string RecordName
        {
            get { return mRecordName; }
        }

        public string ConsoleOutput
        {
            get
            {
                return mPostProcessing != null
                           ? mPostProcessing(mConsoleMirroring.ToString())
                           : mConsoleMirroring.ToString();
            }
        }

        public void Stop()
        {
            mConsoleMirroring.Flush();
            mConsoleMirroring.Dispose();
        }
    }
}