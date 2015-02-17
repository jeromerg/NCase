using Castle.DynamicProxy;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public interface IInvocationRecord
    {
        [NotNull]
        IInvocation Invocation { get; }

        [NotNull]
        ICodeLocation CodeLocation { get; }
    }
}