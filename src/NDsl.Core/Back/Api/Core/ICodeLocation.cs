using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public interface ICodeLocation
    {
        [NotNull]
        string GetUserCodeInfo();
    }
}