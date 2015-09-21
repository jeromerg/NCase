using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPair;

namespace NCase.Imp.Core.Parse
{
    public class AddChildDirector : ActionPairDirector<INode, INode, IAddChildDirector>, IAddChildDirector
    {
        public AddChildDirector(IActionPairVisitMapper<INode, INode, IAddChildDirector> visitMapper)
            : base(visitMapper)
        {
        }
    }
}