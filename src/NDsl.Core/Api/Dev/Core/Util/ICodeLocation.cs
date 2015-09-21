using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Util
{
    public interface ICodeLocation
    {
        [NotNull]
        string GetUserCodeInfo();
    }
}