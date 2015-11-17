using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Util;

namespace NDsl.Front.Api
{
    public interface ICreateContributor : IService<ICaseBuilderModel>
    {
        T Create<T>(ICaseBuilderModel caseBuilderModel, string name);
    }
}