using NDsl.All;

namespace NDsl.Front.Ui
{
    public interface IArtefactApi { }
    public interface IArtefactApi<out TApi> : IArtefactApi
        where TApi : IArtefactApi
    {
        T Toolbox<T>();//where T : ITool<TApi>;
    }
}