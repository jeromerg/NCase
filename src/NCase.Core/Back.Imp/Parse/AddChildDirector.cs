using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPair;

namespace NCaseFramework.Back.Imp.Parse
{
    public class AddChildDirector : ActionPairDirector<INode, INode, IAddChildDirector>, IAddChildDirector
    {
        public AddChildDirector([NotNull] IActionPairVisitMapper<INode, INode, IAddChildDirector> visitMapper)
            : base(visitMapper)
        {
        }
    }
}