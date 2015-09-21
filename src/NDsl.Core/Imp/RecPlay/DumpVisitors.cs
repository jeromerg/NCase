using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NDsl.Api.Dev.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Action;

namespace NDsl.Imp.RecPlay
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, IInterfaceRecPlayNode>
    {
        public void Visit(IDumpDirector director, IInterfaceRecPlayNode node)
        {
            director.AddText("{0}.{1}{2} = {3} ({4})",
                             node.ContributorName,
                             node.PropertyCallKey.PropertyName,
                             BuildIndexesIfExist(node.PropertyCallKey),
                             node.PropertyValue,
                             node.CodeLocation.GetUserCodeInfo());
        }

        private static string BuildIndexesIfExist(PropertyCallKey callKey)
        {
            if (callKey.IndexParameters.Length == 0)
                return "";

            string result = "[";
            foreach (object indexParameter in callKey.IndexParameters)
            {
                result += indexParameter;
                result += ", ";
            }
            result = result.TrimEnd(' ', ',');
            result += "]";

            return result;
        }
    }
}