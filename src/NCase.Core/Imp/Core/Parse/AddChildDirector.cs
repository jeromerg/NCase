using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Core.Nod;
using NVisitor.Api.PairBatch;

namespace NCase.Imp.Core.Parse
{
    public class AddChildDirector : PairDirector<INode, INode, IAddChildDirector>, IAddChildDirector
    {
        public AddChildDirector(IPairVisitMapper<INode, INode, IAddChildDirector> visitMapper) 
            : base(visitMapper)
        {
        }
    }
}