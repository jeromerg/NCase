namespace NTestCase.Api.Dev
{
    public interface IBuilderStrategy
    {
        void PlaceChild(INode<ITarget> parentCandidate, INode<ITarget> child);
    }
}