using System.Linq;
using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.Pairwise
{
    public class AddChildVisitors
        : IAddChildVisitor<IPairwiseNode, INode>,
          IAddChildVisitor<IPairwiseNode, IInterfaceRecPlayNode>
    {
        public void Visit(IAddChildDirector dir, IPairwiseNode parent, IInterfaceRecPlayNode child)
        {
            INode lastSet = parent.Children.LastOrDefault();

            var lastPairwiseDimNode = lastSet as IPairwiseDimNode;
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

        public void Visit(IAddChildDirector dir, IPairwiseNode parent, INode child)
        {
            parent.AddChild(child);
        }
    }
}