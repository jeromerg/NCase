namespace NCase.Back.Api.Core
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}