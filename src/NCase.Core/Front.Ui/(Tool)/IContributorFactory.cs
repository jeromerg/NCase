using NCase.Front.Api;
using NDsl.All;

namespace NCase.Front.Ui
{
    public interface IContributorFactory : ITool<IBuilderModel>
    {
        T Create<T>(IBuilderModel builderModel, string name);
    }
}