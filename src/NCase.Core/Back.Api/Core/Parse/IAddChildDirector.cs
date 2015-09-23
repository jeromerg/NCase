using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPair;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IAddChildDirector : IActionPairDirector<INode, INode, IAddChildDirector>
    {
    }
}