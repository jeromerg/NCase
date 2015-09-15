using NDsl.Api.Dev.Core;

namespace NDsl.Api.Dev.RecPlay
{
    public interface IInterfaceRecPlayContributorFactory
    {
        T CreateContributor<T>(ITokenWriter tokenWriter, string contributorName);
    }
}
