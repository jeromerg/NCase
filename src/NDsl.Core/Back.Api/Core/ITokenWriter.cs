using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public interface ITokenWriter
    {
        void Append([NotNull] IToken token);
    }
}