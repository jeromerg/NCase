using NCase.Back.Api.SetDef;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.SetDef
{
    public interface IPrintDef
        : IService<ISetDefModel<ISetDefId>>

    {
        string Perform(ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive);
    }
}