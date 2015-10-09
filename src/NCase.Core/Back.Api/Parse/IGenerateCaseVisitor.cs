using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NVisitor.Api.FuncPayload;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateCaseVisitor<TNod>
        : IFuncPayloadVisitor<INode, IGenerateCasesDirector, TNod, GenerateOptions, IEnumerable<List<INode>>>
        where TNod : INode
    {
    }
}