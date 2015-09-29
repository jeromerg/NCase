using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ILocated
    {
        [NotNull] ICodeLocation CodeLocation { get; }
    }
}