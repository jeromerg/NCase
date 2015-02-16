using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Batch;

namespace NDsl.Impl.RecPlay
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, IRecPlayInterfacePropertyNode>
    {

        public void Visit(IDumpDirector director, IRecPlayInterfacePropertyNode node)
        {
            director.AddText("{0}.{1}{2} = {3} ({4})", 
                node.ContributorName,
                node.PropertyCallKey.PropertyName,
                BuildIndexesIfExist(node.PropertyCallKey),
                node.Value,
                node.CodeLocation.GetUserCodeInfo());
        }

        private static string BuildIndexesIfExist(PropertyCallKey callKey)
        {
            if (callKey.IndexParameters.Length == 0)
                return "";

            string result = "[";
            foreach (var indexParameter in callKey.IndexParameters)
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
