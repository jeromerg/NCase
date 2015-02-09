using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Util
{
    public interface ICodeLocationUtil
    {
        [CanBeNull]
        ICodeLocation GetUserCodeLocation();
    }
}