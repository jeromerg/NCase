using System.Collections.Generic;
using NDsl.Api.Core;
using NVisitor.Api.Func;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateCaseVisitor<TNod> : IFuncVisitor<INode, IGenerateDirector, TNod, IEnumerable<List<INode>>>
        where TNod : INode
    {
    }
}