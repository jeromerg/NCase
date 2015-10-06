using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInvocationRecord : ILocated
    {
        string InvocationTargetName { get; }
        [NotNull] IInvocation Invocation { get; }
    }
}