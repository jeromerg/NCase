using System.Collections.Generic;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Parse
{
    public interface IParserGenerator
    {
        INode Parse(IDefId def, ITokenReader tokenReader);
        IEnumerable<List<INode>> Generate(INode caseSetNode);
    }
}