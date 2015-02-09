using Castle.DynamicProxy;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Api.RecPlay
{
    public interface IRecPlayInterfacePropertyNode : INode
    {
        [NotNull] object Value { get; }
        [NotNull] ICodeLocation CodeLocation { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        string ContributorName { get; }

        void Replay();
    }
}