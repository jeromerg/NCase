using NDsl.Back.Api.TokenStream;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayContributorFactory
    {
        T CreateContributor<T>(ITokenWriter tokenWriter, string contributorName);
    }
}