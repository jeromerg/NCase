using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IPrintCaseTable
        : IService<ISetDefModel<ISetDefId>>
    {
        string Perform(ISetDefModel<ISetDefId> setDefModel, bool isRecursive);
    }
}