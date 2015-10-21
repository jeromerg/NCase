using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPair;

namespace NCaseFramework.Back.Api.Parse
{
    public interface IAddChildDirector : IActionPairDirector<INode, INode, IAddChildDirector>
    {
    }
}