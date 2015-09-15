using Castle.DynamicProxy;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public interface IInvocationRecord : ILocated
    {
        [NotNull]
        IInvocation Invocation { get; }
    }
}