using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IRecPlayContributorFactory
    {
        T CreateInterface<T>(IAstRoot astRoot, string contributorName);
    }
}
