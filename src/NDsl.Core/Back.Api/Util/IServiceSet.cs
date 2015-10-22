namespace NDsl.Back.Api.Util
{
    public interface IServiceSet<out TClass>
    {
        TTool GetService<TTool>() where TTool : IService<TClass>;
    }
}