using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NDsl.Impl.RecPlay
{
    public class RePlayDir : Director<INode, IRePlayDir>, IRePlayDir
    {
        public RePlayDir(IVisitMapper<INode, IRePlayDir> visitMapper) : base(visitMapper)
        {
        }
    }
}