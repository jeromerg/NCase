using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ICodeLocationUtil
    {
        [NotNull]
        ICodeLocation GetCurrentUserCodeLocation();
    }
}