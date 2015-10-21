using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPair;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IAddChildVisitor<TNodParent, TNodChild>
        : IActionPairVisitor<INode, INode, IAddChildDirector, TNodParent, TNodChild>
        where TNodParent : INode
        where TNodChild : INode
    {
    }
}