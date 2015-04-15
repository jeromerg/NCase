using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IRecPlayContributorFactory
    {
        T CreateInterface<T>(ITokenWriter tokenWriter, string contributorName);
    }
}
