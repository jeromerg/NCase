using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class AddChildVisitors
        : IAddChildVisitor<IPairwiseNode, INode>,
          IAddChildVisitor<IPairwiseNode, IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IAddChildDirector dir, [NotNull] IPairwiseNode parent, [NotNull] IInterfaceRecPlayNode child)
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

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] IPairwiseNode parent, [NotNull] INode child)
        {
            parent.AddChild(child);
        }
    }
}