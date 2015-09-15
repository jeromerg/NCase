using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core
{
    public interface ILocated
    {
        [NotNull]
        ICodeLocation CodeLocation { get; }
    }
}