using NCase.Front.Api;
using NDsl.All;

namespace NCase.Front.Ui
{
    public interface IContributorFactory : ITool<IBuilderApi>
    {
        T Create<T>(IBuilderApi builderApi, string name);
    }
}