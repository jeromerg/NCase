using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IPrintDef 
        : ITool<ISetDefApi<ISetDefApi, ISetDefId>>

    {
        string Perform(ISetDefApi<ISetDefApi, ISetDefId> setDefApi, bool isFileInfo, bool isRecursive);
    }
}