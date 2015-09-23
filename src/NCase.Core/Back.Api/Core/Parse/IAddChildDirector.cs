using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPair;

namespace NCase.Back.Api.Core.Parse
{
    public interface IAddChildDirector : IActionPairDirector<INode, INode, IAddChildDirector>
    {
    }
}