using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface INodifyDir : ILazyDirector<INode, INodifyDir>
    {
    }
}