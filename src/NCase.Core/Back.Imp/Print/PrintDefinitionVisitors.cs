using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<INode>,
          IPrintDefinitionVisitor<IRefNode<IDefNode>>
    {
        /// <summary> If node unknown, then recurse...</summary>
        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] INode node)
        {
            foreach (INode child in node.Children)
                dir.Visit(child);
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IRefNode<IDefNode> node)
        {
            IDefId defId = node.Reference.Id;

            if (dir.IsRecursive)
                dir.Visit(node.Reference);
            else
                dir.PrintLine(node.CodeLocation, "Ref to {0} '{1}'", defId.TypeName, defId.Name);
        }
    }
}