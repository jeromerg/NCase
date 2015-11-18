using JetBrains.Annotations;

namespace NUtil.Doc
{
    public class Snippet
    {
        [NotNull] private readonly string mSource;
        [NotNull] private readonly string mName;
        [NotNull] private readonly string mBody;

        public Snippet([NotNull] string source, [NotNull] string name, [NotNull] string body)
        {
            mSource = source;
            mName = name;
            mBody = body;
        }

        [NotNull]
        public string Source
        {
            get { return mSource; }
        }

        [NotNull]
        public string Name
        {
            get { return mName; }
        }

        [NotNull]
        public string Body
        {
            get { return mBody; }
        }
    }
}