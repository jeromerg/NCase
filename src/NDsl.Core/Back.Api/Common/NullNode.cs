using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    /// <summary>Null Node object, following Null Object Pattern</summary>
    public class NullNode : INode
    {
        public static readonly NullNode Instance = new NullNode();

        private NullNode()
        {
        }

        [NotNull] 
        public CodeLocation CodeLocation
        {
            get { return CodeLocation.Unknown; }
        }

        [NotNull, ItemNotNull] 
        public IEnumerable<INode> Children
        {
            get { return Enumerable.Empty<INode>(); }
        }
    }
}