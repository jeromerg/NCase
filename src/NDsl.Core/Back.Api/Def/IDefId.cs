using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.Def
{
    public interface IDefId : ITypedId
    {
        [NotNull] string DefTypeName { get; }
        [NotNull] string Name { get; }
    }
}