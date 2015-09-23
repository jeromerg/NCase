using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public interface ICodeLocation
    {
        [NotNull]
        string GetUserCodeInfo();
    }
}