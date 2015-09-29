using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public interface ITokenWriter
    {
        void Append([NotNull] IToken token);
    }
}