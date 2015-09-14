using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Tree;
using NCase.Imp.Prod;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;

namespace NCase.Imp.Tree
{
    public class AddChildVisitors
        : IAddChildVisitor<ITreeNode, INode>
        , IAddChildVisitor<ITreeNode, ProdDimNode>
        , IAddChildVisitor<ITreeNode, IInterfaceRecPlayNode>
    {
        public void Visit(IAddChildDirector director, ITreeNode scopeParent, INode childCandidate)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IAddChildDirector director, ITreeNode scopeParent, ProdDimNode childCandidate)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(IAddChildDirector director, ITreeNode scopeParent, IInterfaceRecPlayNode childCandidate)
        {
            throw new System.NotImplementedException();
        }
    }
}