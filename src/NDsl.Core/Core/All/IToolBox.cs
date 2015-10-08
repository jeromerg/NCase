namespace NDsl.All
{
    public interface IToolBox<TClass>
    {
        T GetTool<T>();// where T : ITool<TClass>;
    }
}
