using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.Fact
{
    public interface IReplayFact : IService<IFactModel>
    {
        void Perform(IFactModel factModel, bool iReplay);
    }
}