using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface IDefId : ITypedId
    {
        [NotNull] string DefTypeName { get; }
        [NotNull] string Name { get; }
    }
}