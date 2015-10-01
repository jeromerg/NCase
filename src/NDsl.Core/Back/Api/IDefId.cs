using JetBrains.Annotations;

namespace NDsl.Back.Api
{
    public interface IDefId : ITypedId
    {
        [NotNull] string Name { get; }
    }
}