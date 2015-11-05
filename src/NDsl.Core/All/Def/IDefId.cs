using JetBrains.Annotations;
using NDsl.All.Common;
using NDsl.Back.Api.Common;

namespace NDsl.All.Def
{
    public interface IDefId : ITypedId
    {
        [NotNull] string DefTypeName { get; }
        [NotNull] string Name { get; }
    }
}