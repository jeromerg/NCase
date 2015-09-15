using NDsl.Api.Dev.Core.Nod;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.RecPlay
{
    public interface IInterfaceRecPlayNode : INode
    {
        [CanBeNull] object PropertyValue { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        string ContributorName { get; }

        bool IsReplay { get; set; }
    }
}