using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Util
{
    public interface ICodeLocationUtil
    {
        [NotNull]
        ICodeLocation GetCurrentUserCodeLocation();
    }
}