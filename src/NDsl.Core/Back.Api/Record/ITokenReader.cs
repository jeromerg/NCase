using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Record
{
    public interface ITokenReader : IRecorder
    {
        [NotNull, ItemNotNull] IEnumerable<IToken> Tokens { get; }
    }
}