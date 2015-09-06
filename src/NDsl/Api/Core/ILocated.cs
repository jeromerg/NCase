using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Core
{
    public interface ILocated
    {
        [NotNull]
        ICodeLocation CodeLocation { get; }
    }
}