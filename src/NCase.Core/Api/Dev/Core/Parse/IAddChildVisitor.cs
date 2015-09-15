using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPair;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IAddChildVisitor<TNodParent, TNodChild> 
        : IActionPairVisitor<INode, INode, IAddChildDirector, TNodParent, TNodChild>        
        where TNodParent : INode
        where TNodChild : INode 
    {
    }
}
