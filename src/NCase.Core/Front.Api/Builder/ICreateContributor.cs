using NDsl.Back.Api.Util;

namespace NCase.Front.Api.Builder
{
    public interface ICreateContributor : IService<IBuilderModel>
    {
        T Create<T>(IBuilderModel builderModel, string name);
    }
}