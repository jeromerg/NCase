using JetBrains.Annotations;
using NDsl.All;

namespace NDsl.Front.Ui
{
    public interface IApi<out TModel>
    {
        [NotNull] TModel Model { get; }
        IServices<TModel> Services { get; }
    }
}