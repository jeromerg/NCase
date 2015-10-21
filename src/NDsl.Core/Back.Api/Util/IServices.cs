namespace NDsl.Back.Api.Util
{
    public interface IServices<out TClass>
    {
        TTool GetService<TTool>() where TTool : IService<TClass>;
    }
}