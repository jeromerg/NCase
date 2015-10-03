using System.Collections.Generic;
using System.Linq;

namespace NDsl.Back.Api.Core
{
    /// <summary>Null Node object, following Null Object Pattern</summary>
    public class NullNode : INode
    {
        public static readonly NullNode Instance = new NullNode();

        private NullNode()
        {
        }

        public CodeLocation CodeLocation
        {
            get { return CodeLocation.Unknown; }
        }

        public IEnumerable<INode> Children
        {
            get { return Enumerable.Empty<INode>(); }
        }
    }
}