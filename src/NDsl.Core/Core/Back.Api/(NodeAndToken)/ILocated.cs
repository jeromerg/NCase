using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ILocated
    {
        [NotNull] CodeLocation CodeLocation { get; }
    }
}