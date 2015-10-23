namespace NCaseFramework.doc
{
    public class ConsoleRecord
    {
        private readonly string mCallerMemberName;
        private readonly string mCallerFilePath;
        private readonly int mCallerLineNumber;
        private readonly string mText;

        public ConsoleRecord(string text, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
            mCallerMemberName = callerMemberName;
            mCallerFilePath = callerFilePath;
            mCallerLineNumber = callerLineNumber;
            mText = text;
        }

        public string CallerMemberName
        {
            get { return mCallerMemberName; }
        }

        public string CallerFilePath
        {
            get { return mCallerFilePath; }
        }

        public int CallerLineNumber
        {
            get { return mCallerLineNumber; }
        }

        public string Text
        {
            get { return mText; }
        }
    }
}