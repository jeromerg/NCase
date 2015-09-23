using System.Collections.Generic;
using NCase.All;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Back.Api.Core.Parse
{
    public interface IParserGenerator
    {
        IEnumerable<List<INode>> ParseAndGenerate(IDefId def, ITokenReader tokenReader);
    }
}