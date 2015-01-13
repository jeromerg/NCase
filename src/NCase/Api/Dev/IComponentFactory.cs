namespace NTestCase.Api.Dev
{
    public interface IComponentFactory
    {
        bool CanHandle<T>();
        T Create<T>(INode<ITarget> rootNode, IBuilderStrategy builderStrategy, object[] arguments);
    }
}