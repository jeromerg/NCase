using NCaseFramework.Front.Api.Case;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IPrintCase : IService<ICaseModel>
    {
        string Perform(ICaseModel caseModel);
    }
}