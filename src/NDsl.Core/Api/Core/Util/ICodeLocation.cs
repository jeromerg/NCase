using NVisitor.Common.Quality;

namespace NDsl.Api.Core.Util
{
    public interface ICodeLocation
    {
        [NotNull]
        string GetUserCodeInfo();
    }
}