using JetBrains.Annotations;
using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IInterfaceReIInterfaceRecPlayNodeFactory
    {
        IInterfaceRecPlayNode Create([NotNull] IInterfaceRecPlayInterceptor parentInterceptor,
                                     [NotNull] string contributorName,
                                     [NotNull] PropertyCallKey propertyCallKey,
                                     [CanBeNull] object propertyValue,
                                     [NotNull] ICodeLocation codeLocation);
    }
}