using Castle.DynamicProxy;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInvocationRecord : ILocated
    {
        [NotNull] IInvocation Invocation { get; }
    }
}