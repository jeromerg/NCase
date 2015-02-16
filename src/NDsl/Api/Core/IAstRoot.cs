using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    public interface IAstRoot : INodeWithChildren
    {
        AstState State { get; set; }

        void AddChild([NotNull] INode child);
    }
}