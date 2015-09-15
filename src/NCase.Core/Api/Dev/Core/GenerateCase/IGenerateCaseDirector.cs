using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Func;

namespace NCase.Api.Dev.Core.GenerateCase
{
    public interface IGenerateCaseDirector : IFuncDirector<INode, IGenerateCaseDirector, IEnumerable<List<INode>>>
    {
    }
}