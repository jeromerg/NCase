namespace NDsl.Api.Dev
{
    public interface IContributorFactory
    {
        bool CanHandle<T>();
        T Create<T>(RootNode rootNode, object[] arguments);
    }
}