namespace NCase.Api.Dev
{
    public interface IContribFactory
    {
        bool CanHandle<T>();
        T Create<T>(AstNode astNode, object[] arguments);
    }
}