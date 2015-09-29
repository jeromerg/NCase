using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInvocationRecord : ILocated
    {
        [NotNull] IInvocation Invocation { get; }
    }
}