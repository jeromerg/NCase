using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public interface ILocated
    {
        [NotNull] ICodeLocation CodeLocation { get; }
    }
}