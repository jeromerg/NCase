using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Util
{
    public static class ListUtil
    {
        public static List<INode> Concat([NotNull] List<INode> nodes, [NotNull] List<INode> subnodes)
        {
            var result = new List<INode>(nodes);
            result.AddRange(subnodes);
            return result;
        }
    }
}