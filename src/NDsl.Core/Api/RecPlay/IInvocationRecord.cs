using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IInvocationRecord : ILocated
    {
        [NotNull] IInvocation Invocation { get; }
    }
}