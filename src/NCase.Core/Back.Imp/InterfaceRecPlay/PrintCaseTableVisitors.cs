using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseTableVisitors : IPrintCaseTableVisitor<IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IPrintCaseTableDirector director, [NotNull] IInterfaceRecPlayNode node)
        {
            director.Print(node.CodeLocation,
                           new InterfaceRecPlayNodeColumn(node),
                           node.PropertyValue != null ? node.PropertyValue.ToString() : "null");
        }
    }
}