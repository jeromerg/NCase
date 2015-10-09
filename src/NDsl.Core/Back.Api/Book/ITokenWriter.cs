using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Book
{
    public interface ITokenWriter
    {
        void Append([NotNull] IToken token);
    }
}