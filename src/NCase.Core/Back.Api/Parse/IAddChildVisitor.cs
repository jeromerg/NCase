using NDsl.Back.Api.Core;
using NVisitor.Api.ActionPair;

namespace NCase.Back.Api.Parse
{
    public interface IAddChildVisitor<TNodParent, TNodChild>
        : IActionPairVisitor<INode, INode, IAddChildDirector, TNodParent, TNodChild>
        where TNodParent : INode
        where TNodChild : INode
    {
    }
}