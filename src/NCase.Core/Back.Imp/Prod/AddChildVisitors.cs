using System.Linq;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Prod;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.Prod
{
    public class AddChildVisitors
        : IAddChildVisitor<IProdNode, INode>,
          IAddChildVisitor<IProdNode, IInterfaceRecPlayNode>
    {
        public void Visit(IAddChildDirector dir, IProdNode parent, IInterfaceRecPlayNode child)
        {
            INode lastSet = parent.Children.LastOrDefault();

            var lastProdDimNode = lastSet as ProdDimNode;
            if (lastProdDimNode == null)
            {
                parent.AddChild(new ProdDimNode(child));
                return;
            }

            var lastInterfaceRecPlayNode = lastProdDimNode.FirstChild as IInterfaceRecPlayNode;
            if (lastInterfaceRecPlayNode == null)
            {
                parent.AddChild(new ProdDimNode(child));
                return;
            }

            if (!Equals(lastInterfaceRecPlayNode.PropertyCallKey, child.PropertyCallKey))
            {
                parent.AddChild(new ProdDimNode(child));
                return;
            }

            lastProdDimNode.PlaceNextValue(child);
        }

        public void Visit(IAddChildDirector dir, IProdNode parent, INode child)
        {
            parent.AddChild(child);
        }
    }
}