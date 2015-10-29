namespace NUtil.Doc
{
    public class Snippet
    {
        private readonly string mSource;
        private readonly string mName;
        private readonly string mBody;

        public Snippet(string source, string name, string body)
        {
            mSource = source;
            mName = name;
            mBody = body;
        }

        public string Source
        {
            get { return mSource; }
        }

        public string Name
        {
            get { return mName; }
        }

        public string Body
        {
            get { return mBody; }
        }
    }
}