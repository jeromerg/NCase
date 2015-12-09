using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public class CodeLocation
    {
        [NotNull] public static readonly CodeLocation Unknown = new CodeLocation("unknown", null, null);

        [CanBeNull] private readonly string mFileName;
        [CanBeNull] private readonly int? mLine;
        [CanBeNull] private readonly int? mColumn;

        public CodeLocation([CanBeNull] string fileName, [CanBeNull] int? line, [CanBeNull] int? column)
        {
            mFileName = fileName;
            mLine = line;
            mColumn = column;
        }

        [CanBeNull] public string FileName
        {
            get { return mFileName; }
        }

        public int? Line
        {
            get { return mLine; }
        }

        public int? Column
        {
            get { return mColumn; }
        }
    }
}