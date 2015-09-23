﻿using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.All.Helper
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