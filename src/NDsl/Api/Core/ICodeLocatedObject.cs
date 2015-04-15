using NDsl.Api.Core.Util;

namespace NDsl.Api.Core
{
    public interface ICodeLocatedObject
    {
        ICodeLocation CodeLocation { get; }
    }
}