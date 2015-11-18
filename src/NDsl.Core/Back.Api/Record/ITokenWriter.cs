using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface ITokenWriter : IRecorder
    {
        void Append([NotNull] IToken token);
    }
}