using System.Collections.Generic;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Parse
{
    public interface IParserGenerator
    {
        IEnumerable<List<INode>> ParseAndGenerate(IDefId def, ITokenReader tokenReader);
    }
}