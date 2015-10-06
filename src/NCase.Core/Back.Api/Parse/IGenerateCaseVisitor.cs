using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NVisitor.Api.FuncPayload;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateCaseVisitor<TNod>
        : IFuncPayloadVisitor<INode, IGenerateCasesDirector, TNod, GenerateOptions, IEnumerable<List<INode>>>
        where TNod : INode
    {
    }
}