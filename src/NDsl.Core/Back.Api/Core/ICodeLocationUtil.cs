using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public interface ICodeLocationUtil
    {
        [NotNull]
        ICodeLocation GetCurrentUserCodeLocation();
    }
}