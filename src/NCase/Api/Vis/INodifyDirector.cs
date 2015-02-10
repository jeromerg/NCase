using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Api.Vis
{
    public interface INodifyDirector : ILazyDirector<INode, INodifyDirector>
    {
    }
}