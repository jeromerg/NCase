using System.Collections.Generic;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;

namespace NCase.Back.Api.Parse
{
    public interface IParserGenerator
    {
        INode Parse(IDefId def, ITokenReader tokenReader);
        IEnumerable<List<INode>> Generate(INode caseSetNode, GenerateOptions options);
    }
}