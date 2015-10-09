using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NVisitor.Api.FuncPayload;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateCasesDirector
        : IFuncPayloadDirector<INode, IGenerateCasesDirector, GenerateOptions, IEnumerable<List<INode>>>
    {
    }
}