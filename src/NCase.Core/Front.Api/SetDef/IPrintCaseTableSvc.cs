using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IPrintCaseTableSvc
        : IService<ISetDefModel<ISetDefId>>
    {
        [NotNull] 
        string Perform([NotNull] ISetDefModel<ISetDefId> setDefModel, bool isRecursive);
    }
}