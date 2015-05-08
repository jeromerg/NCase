using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface ITreeCaseSetInsertChildDirector : IDirector<INode, ITreeCaseSetInsertChildDirector>
    {
        void InitializeRoot(INode root);
        INode Root { get; }
    }
}