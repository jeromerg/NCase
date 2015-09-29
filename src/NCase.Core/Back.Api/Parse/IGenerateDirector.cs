using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NVisitor.Api.Func;

namespace NCase.Back.Api.Parse
{
    public interface IGenerateDirector : IFuncDirector<INode, IGenerateDirector, IEnumerable<List<INode>>>
    {
    }
}