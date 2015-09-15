using System.Linq;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.RecPlay;

namespace NCase.Imp.Pairwise
{
    public class AddChildVisitors
        : IAddChildVisitor<IPairwiseNode, INode>
        , IAddChildVisitor<IPairwiseNode, IInterfaceRecPlayNode>
    {
        public void Visit(IAddChildDirector dir, IPairwiseNode parent, INode child)
        {
            parent.AddChild(child);
        }

        public void Visit(IAddChildDirector dir, IPairwiseNode parent, IInterfaceRecPlayNode child)
        {
            INode lastSet = parent.Children.LastOrDefault();

            var lastPairwiseDimNode = lastSet as PairwiseDimNode;
            if (lastPairwiseDimNode == null)
            {
                parent.AddChild(new PairwiseDimNode(child));
                return;
            }

            var lastInterfaceRecPlayNode = lastPairwiseDimNode.FirstChild as IInterfaceRecPlayNode;
            if (lastInterfaceRecPlayNode == null)
            {
                parent.AddChild(new PairwiseDimNode(child));
                return;
            }

            if (!Equals(lastInterfaceRecPlayNode.PropertyCallKey, child.PropertyCallKey))
            {
                parent.AddChild(new PairwiseDimNode(child));
                return;
            }

            lastPairwiseDimNode.PlaceNextValue(child);
        }
    }
}