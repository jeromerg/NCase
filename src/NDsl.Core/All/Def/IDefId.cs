using JetBrains.Annotations;
using NDsl.All.Common;

namespace NDsl.All.Def
{
    public interface IDefId : ITypedId
    {
        [NotNull] string DefTypeName { get; }
        [NotNull] string Name { get; }
    }
}