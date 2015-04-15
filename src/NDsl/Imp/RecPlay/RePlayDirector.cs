using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NDsl.Imp.RecPlay
{
    public class RePlayDirector : Director<INode, IRePlayDirector>, IRePlayDirector
    {
        public RePlayDirector(IVisitMapper<INode, IRePlayDirector> visitMapper) : base(visitMapper)
        {
        }
    }
}