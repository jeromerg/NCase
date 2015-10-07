using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayContributorFactory
    {
        T CreateContributor<T>(ITokenWriter tokenWriter, string contributorName);
    }
}