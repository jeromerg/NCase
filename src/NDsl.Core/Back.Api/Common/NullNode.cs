using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    /// <summary>Null Node object, following Null Object Pattern</summary>
    public class NullNode : INode
    {
        [NotNull]
        public static readonly NullNode Instance = new NullNode(CodeLocation.Unknown);

        [NotNull] private readonly CodeLocation mCodeLocation;

        public NullNode([NotNull] CodeLocation codeLocation)
        {
            mCodeLocation = codeLocation;
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        [NotNull, ItemNotNull] public IEnumerable<INode> Children
        {
            get { return Enumerable.Empty<INode>(); }
        }
    }
}