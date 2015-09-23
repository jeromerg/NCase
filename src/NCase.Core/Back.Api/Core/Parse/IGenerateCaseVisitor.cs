using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Back.Api.Core.Parse
{
    public interface IGenerateCaseVisitor<TNod> : IFuncVisitor<INode, IGenerateDirector, TNod, IEnumerable<List<INode>>>
        where TNod : INode
    {
    }
}