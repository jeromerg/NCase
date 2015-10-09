namespace NDsl.Back.Api.Util
{
    public interface IServices<out TClass>
    {
        TTool GetTool<TTool>() where TTool : IService<TClass>;
    }
}
