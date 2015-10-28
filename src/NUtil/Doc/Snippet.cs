namespace NCaseFramework.doc
{
    public class Snippet
    {
        private readonly string mName;
        private readonly string mBody;

        public Snippet(string name, string body)
        {
            mName = name;
            mBody = body;
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