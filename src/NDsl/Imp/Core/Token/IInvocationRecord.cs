using Castle.DynamicProxy;
using NDsl.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public interface IInvocationRecord : ILocated
    {
        [NotNull]
        IInvocation Invocation { get; }
    }
}