using NCase.Back.Api.Parse;
using NDsl.Api.Core;
using NVisitor.Api.ActionPair;

namespace NCase.Back.Imp.Parse
{
    public class AddChildDirector : ActionPairDirector<INode, INode, IAddChildDirector>, IAddChildDirector
    {
        public AddChildDirector(IActionPairVisitMapper<INode, INode, IAddChildDirector> visitMapper)
            : base(visitMapper)
        {
        }
    }
}