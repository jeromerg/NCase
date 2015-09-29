using System.Text;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintDetailsVisitors : IPrintDetailsVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintDetailsDirector dir, IInterfaceRecPlayNode node, StringBuilder sb)
        {
            sb.AppendFormat("{0}\n  {1}.{2}={3}",
                            node.CodeLocation.GetUserCodeInfo(),
                            node.ContributorName,
                            Reconstitute(node.PropertyCallKey),
                            node.PropertyValue);
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