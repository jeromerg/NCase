using System.Collections.Generic;
using NDsl.Back.Api.Common;
using NVisitor.Api.FuncPayload;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IGenerateCasesDirector
        : IFuncPayloadDirector<INode, IGenerateCasesDirector, GenerateOptions, IEnumerable<List<INode>>>
    {
    }
}