using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface ICodeLocationUtil
    {
        [NotNull]
        CodeLocation GetCurrentUserCodeLocation();
    }
}