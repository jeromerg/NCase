using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IGenerateDirector : IFuncDirector<INode, IGenerateDirector, IEnumerable<List<INode>>>
    {
    }
}