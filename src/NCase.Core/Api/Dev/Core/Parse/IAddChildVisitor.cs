using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.PairBatch;

namespace NCase.Api.Dev.Core.Parse
{
    public interface IAddChildVisitor<TNodParent, TNodChild> 
        : IPairVisitor<INode, INode, IAddChildDirector, TNodParent, TNodChild>        
        where TNodParent : INode
        where TNodChild : INode 
    {
    }
}
