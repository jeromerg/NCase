using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    public interface ILocated
    {
        [NotNull] CodeLocation CodeLocation { get; }
    }
}