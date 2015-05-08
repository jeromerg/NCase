using NDsl.Api.Core.Util;

namespace NDsl.Api.Core
{
    public interface ILocated
    {
        ICodeLocation CodeLocation { get; }
    }
}