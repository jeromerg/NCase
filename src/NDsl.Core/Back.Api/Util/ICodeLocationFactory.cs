using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface ICodeLocationFactory
    {
        [NotNull]
        CodeLocation GetCurrentUserCodeLocation();
    }
}