using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface ITreeCaseSetInsertChildDirector : IDirector<INode, ITreeCaseSetInsertChildDirector>
    {
        void InitializeCurrentParentCandidate(INode candidate);
        INode CurrentParentCandidate { get; }
    }
}