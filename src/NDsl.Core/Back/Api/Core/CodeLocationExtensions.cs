using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public static class CodeLocationExtensions
    {
        [NotNull]
        public static string GetFullInfo(this CodeLocation loc)
        {
            return string.Format("{0}{1}",
                                 loc.FileName ?? "unknown file",
                                 GetLineAndColumnInfo(loc));
        }

        [NotNull]
        public static string GetLineAndColumnInfo(this CodeLocation loc)
        {
            return string.Format("({0}:{1})",
                                 loc.Line.HasValue ? loc.Line.Value.ToString() : "??",
                                 loc.Column.HasValue ? loc.Column.Value.ToString() : "??");
        }
    }
}