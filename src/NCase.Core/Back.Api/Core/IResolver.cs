namespace NCase.Api.Dev.Core
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}
