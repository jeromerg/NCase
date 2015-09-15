using NDsl.Api.Dev.Core.Tok;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core
{
    public interface ITokenWriter
    {
        void Append([NotNull] IToken token);
    }
}