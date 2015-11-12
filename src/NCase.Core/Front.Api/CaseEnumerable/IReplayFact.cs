using NCaseFramework.Front.Api.Fact;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.CaseEnumerable
{
    public interface IReplayFact : IService<IFactModel>
    {
        void Perform(IFactModel factModel, bool iReplay);
    }
}