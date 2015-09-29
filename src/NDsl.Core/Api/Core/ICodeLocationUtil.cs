using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public interface ICodeLocationUtil
    {
        [NotNull]
        ICodeLocation GetCurrentUserCodeLocation();
    }
}