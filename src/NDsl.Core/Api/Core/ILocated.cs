

using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public interface ILocated
    {
        [NotNull] ICodeLocation CodeLocation { get; }
    }
}