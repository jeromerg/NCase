using JetBrains.Annotations;

namespace NDsl.Api.Core
{
    public interface ICodeLocation
    {
        [NotNull]
        string GetUserCodeInfo();
    }
}