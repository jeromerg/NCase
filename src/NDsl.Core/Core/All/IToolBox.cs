namespace NDsl.All
{
    public interface IToolBox<out TClass>
    {
        TTool GetTool<TTool>() where TTool : ITool<TClass>;
    }
}
