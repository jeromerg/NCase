using JetBrains.Annotations;
using NDsl.Back.Api.Builder;
using NDsl.Back.Api.Util;

namespace NDsl.Front.Api
{
    public interface ICreateContributor : IService<ICaseBuilderModel>
    {
        [NotNull] 
        T Create<T>([NotNull] ICaseBuilderModel caseBuilderModel, [NotNull] string name);
    }
}