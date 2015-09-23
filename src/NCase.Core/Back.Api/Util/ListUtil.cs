using System.Collections.Generic;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Util
{
    public static class ListUtil
    {
        public static List<INode> Concat(List<INode> nodes, List<INode> subnodes)
        {
            var result = new List<INode>(nodes);
            result.AddRange(subnodes);
            return result;
        }
    }
}