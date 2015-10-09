namespace NDsl.All
{
    public interface IServices<out TClass>
    {
        TTool GetTool<TTool>() where TTool : ITool<TClass>;
    }
}
