using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Util
{
    public interface ICodeLocationUtil
    {
        [NotNull] ICodeLocation GetCurrentUserCodeLocation();
    }
}