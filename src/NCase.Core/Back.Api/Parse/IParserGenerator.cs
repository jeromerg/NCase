using System.Collections.Generic;
using NCase.Back.Api.Core;
using NDsl.Api.Core;

namespace NCase.Back.Api.Parse
{
    public interface IParserGenerator
    {
        IEnumerable<List<INode>> ParseAndGenerate(IDefId def, ITokenReader tokenReader);
    }
}