using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NVisitor.Api.FuncPayload;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateCasesDirector
        : IFuncPayloadDirector<INode, IGenerateCasesDirector, GenerateOptions, IEnumerable<List<INode>>>
    {
    }
}