using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Front.Api
{
    public interface IApi<out TModel>
    {
        [NotNull] TModel Model { get; }
        IServiceSet<TModel> Services { get; }
    }
}