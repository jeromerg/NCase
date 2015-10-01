using NCase.Back.Api.Print;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Tree
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ITreeNode>
    {
        public void Visit(IPrintDefinitionDirector dir, ITreeNode node)
        {
            if (dir.IncludeFilePath)
                dir.Print(node.CodeLocation.GetFullInfo());

            if(node.Fact == null)
                dir.Print("TREE {0}{1}", node.Id.Name, node.CodeLocation.GetLineAndColumnInfo());
            else
                dir.Visit(node.Fact);
            
            // ---

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}