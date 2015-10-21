using NCaseFramework.Back.Api.SetDef;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IGetCases
        : IService<ISetDefModel<ISetDefId>>
    {
        Ui.CaseEnumerable Perform(ISetDefModel<ISetDefId> setDefModel);
    }
}