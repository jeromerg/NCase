using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IReplayFactSvc : IService<IFactModel>
    {
        void Perform([NotNull] IFactModel factModel, bool isReplay);
    }
}