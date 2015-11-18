using System.Collections.Generic;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IParserGenerator
    {
        INode Parse(IDefId def, ITokenReader tokenReader);
        IEnumerable<List<INode>> Generate(INode caseSetNode, GenerateOptions options);
    }
}