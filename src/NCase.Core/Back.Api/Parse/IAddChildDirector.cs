using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPair;

namespace NCase.Back.Api.Parse
{
    public interface IAddChildDirector : IActionPairDirector<INode, INode, IAddChildDirector>
    {
    }
}