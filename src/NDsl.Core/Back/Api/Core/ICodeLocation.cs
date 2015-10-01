using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ICodeLocation
    {
        [CanBeNull] string FileName { get; }

        [CanBeNull] int? Line { get; }

        [CanBeNull] int? Column { get; }

        [NotNull]
        string GetFullInfo();

        [NotNull]
        string GetLineAndColumnInfo();
    }
}