using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Api.Dev.Core.GenerateCase
{
    public interface IGenerateCaseVisitor<TNod> : IFuncVisitor<INode, IGenerateCaseDirector, TNod, IEnumerable<List<INode>>>
        where TNod : INode
    {
    }
}