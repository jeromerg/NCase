namespace NDsl.Api.Core.Nod
{
    public interface ITreeCaseSetNode : INode
    {
        void PlaceNextChild(INode child);
    }
}