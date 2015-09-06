using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Api.RecPlay
{
    public interface IInterfaceRecPlayNode : INode
    {
        [CanBeNull] object PropertyValue { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        string ContributorName { get; }

        bool IsReplay { get; set; }
    }
}