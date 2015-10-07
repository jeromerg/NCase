using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ITokenWriter
    {
        void Append([NotNull] IToken token);
    }
}