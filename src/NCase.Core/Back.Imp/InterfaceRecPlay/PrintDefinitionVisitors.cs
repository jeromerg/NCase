using System.Text;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintDefinitionVisitors : IPrintDefinitionVisitor<IInterfaceRecPlayNode>
    {
        /// <summary> If node unknown, then recurse...</summary>
        public void Visit(IPrintDefinitionDirector dir, INode node, StringBuilder sb)
        {
            foreach (INode child in node.Children)
                dir.Visit(child, sb);
        }

        public void Visit(IPrintDefinitionDirector dir, IInterfaceRecPlayNode node, StringBuilder sb)
        {
            node.
            sb.node.PrintAssignment()
        }

        private string Reconstitute(PropertyCallKey propertyCallKey)
        {
            return propertyCallKey.IndexParameters.Length == 0
                       ? propertyCallKey.PropertyName
                       : string.Format("{0}[{1}]",
                                       propertyCallKey.PropertyName,
                                       string.Join(", ", propertyCallKey.IndexParameters));
        }
    }
}