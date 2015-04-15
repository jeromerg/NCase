using Castle.DynamicProxy;
using NDsl.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Token
{
    public interface IInvocationRecord : ICodeLocatedObject
    {
        [NotNull]
        IInvocation Invocation { get; }
    }
}