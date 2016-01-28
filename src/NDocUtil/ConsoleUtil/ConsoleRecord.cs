using System;
using JetBrains.Annotations;

namespace NDocUtilLibrary.ConsoleUtil
{
    public class ConsoleRecord
    {
        [NotNull] private readonly ConsoleMirroring mConsoleMirroring;
        [NotNull] private readonly string mRecordName;
        [CanBeNull] private readonly Func<string, string> mPostProcessing;

        public ConsoleRecord([NotNull] string recordName, [CanBeNull] Func<string, string> postProcessing = null)
        {
            if (recordName == null) throw new ArgumentNullException("recordName");

            mConsoleMirroring = new ConsoleMirroring();
            mRecordName = recordName;
            mPostProcessing = postProcessing;
        }

        [NotNull] public string RecordName
        {
            get { return mRecordName; }
        }

        [NotNull] public string ConsoleOutput
        {
            get
            {
                if (mPostProcessing != null)
                    return mPostProcessing(mConsoleMirroring.ToString()) ?? "";
                else
                    return mConsoleMirroring.ToString();
            }
        }

        public void Stop()
        {
            mConsoleMirroring.Flush();
            mConsoleMirroring.Dispose();
        }
    }
}