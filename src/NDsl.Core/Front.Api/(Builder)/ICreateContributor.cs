using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Util;

namespace NDsl.Front.Api
{
    public interface ICreateContributor : IService<IBuilderModel>
    {
        T Create<T>(IBuilderModel builderModel, string name);
    }
}