namespace NDsl.Api.Dev.Core.Nod
{
    public interface IRefNode<out T> : INode
        where T : INode
    {
        T Reference { get; }
    }
}