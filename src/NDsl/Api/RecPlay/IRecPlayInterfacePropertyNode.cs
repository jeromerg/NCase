using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Api.RecPlay
{
    public interface IRecPlayInterfacePropertyNode : INode
    {
        [CanBeNull] object PropertyValue { get; }
        [NotNull] ICodeLocation CodeLocation { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        string ContributorName { get; }

        bool IsReplay { get; set; }
    }
}