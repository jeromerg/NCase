namespace NDsl.All
{
    public interface IToolBox<out TClass>
    {
        T Resolve<T>() where T : ITool<TClass>;
    }
}
