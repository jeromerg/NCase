using JetBrains.Annotations;

namespace NDsl.All.Common
{
    public interface IId
    {
        [NotNull] string TypeName { get; }
        [NotNull] string Name { get; }
    }
}