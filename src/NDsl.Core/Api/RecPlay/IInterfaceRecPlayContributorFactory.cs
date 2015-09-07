using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IInterfaceRecPlayContributorFactory
    {
        T CreateContributor<T>(ITokenWriter tokenWriter, string contributorName);
    }
}
