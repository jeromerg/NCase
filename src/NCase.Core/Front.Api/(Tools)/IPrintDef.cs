using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IPrintDef 
        : ITool<ISetDefModel<ISetDefId>>

    {
        string Perform(ISetDefModel<ISetDefId> setDefModel, bool isFileInfo, bool isRecursive);
    }
}