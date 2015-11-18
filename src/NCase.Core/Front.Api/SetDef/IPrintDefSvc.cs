using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface IPrintDefSvc
        : IService<ISetDefModel<ISetDefId>>

    {
        [NotNull]
        string PrintDef([NotNull] ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive);
    }
}