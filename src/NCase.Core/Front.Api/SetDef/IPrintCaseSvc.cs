using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IPrintCaseSvc : IService<ICaseModel>
    {
        [NotNull]
        string PrintCase([NotNull] ICaseModel caseModel);
    }
}