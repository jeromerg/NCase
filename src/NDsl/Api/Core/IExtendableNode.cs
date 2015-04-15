namespace NDsl.Api.Core
{
    public interface IExtendableNode : INode
    {
        void AddChild(INode child);
    }
}