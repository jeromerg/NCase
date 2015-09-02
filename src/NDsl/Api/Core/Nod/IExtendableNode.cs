namespace NDsl.Api.Core.Nod
{
    public interface IExtendableNode : INode
    {
        void AddChild(INode child);
    }
}