using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface IServiceSet<out TClass>
    {
        [NotNull]
        TTool GetService<TTool>() where TTool : IService<TClass>;
    }
}