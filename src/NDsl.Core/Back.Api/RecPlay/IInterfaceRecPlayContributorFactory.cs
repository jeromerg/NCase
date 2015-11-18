using JetBrains.Annotations;
using NDsl.Back.Api.Record;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayContributorFactory
    {
        [NotNull] 
        T CreateContributor<T>([NotNull] ITokenWriter tokenWriter, [NotNull] string contributorName);
    }
}