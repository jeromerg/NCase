using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Print
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<INode>,
          IPrintDefinitionVisitor<IRefNode<IDefNode>>
    {
        /// <summary> If node unknown, then recurse...</summary>
        public void Visit(IPrintDefinitionDirector dir, INode node)
        {
            foreach (INode child in node.Children)
                dir.Visit(child);
        }

        public void Visit(IPrintDefinitionDirector dir, IRefNode<IDefNode> node)
        {
            IDefId defId = node.Reference.DefId;

            if (dir.IsRecursive)
                dir.Visit(node.Reference);
            else
                dir.Print(node.CodeLocation, "Ref to {0} '{1}'", defId.DefTypeName, defId.Name);
        }
    }
}