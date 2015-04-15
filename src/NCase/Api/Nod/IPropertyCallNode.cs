using NDsl.Api.Core;
using NDsl.Util.Castle;

namespace NCase.Api.Nod
{
    public interface IPropertyCallNode : INode, ICodeLocatedObject
    {
        PropertyCallKey PropertyCallKey { get; }
    }
}