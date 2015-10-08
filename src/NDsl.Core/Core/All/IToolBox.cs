namespace NDsl.All
{
    public interface IToolBox<out TClass>
    {
        T GetTool<T>();// where T : ITool<TClass>;
    }
}
