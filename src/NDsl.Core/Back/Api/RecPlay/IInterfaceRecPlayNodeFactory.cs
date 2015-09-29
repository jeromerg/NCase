using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
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