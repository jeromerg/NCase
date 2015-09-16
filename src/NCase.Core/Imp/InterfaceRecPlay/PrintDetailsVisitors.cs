﻿using System.Text;
using NCase.Api.Dev.Core.Print;
using NDsl.Api.Dev.RecPlay;
using NDsl.Util.Castle;

namespace NCase.Imp.InterfaceRecPlay
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
                        : string.Format("{0}[{1}]", propertyCallKey.PropertyName, string.Join(", ", propertyCallKey.IndexParameters));
        }
    }
}
