using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IPrintTable
        : ITool<ISetDefModel<ISetDefId>>
    {
        string Perform(ISetDefModel<ISetDefId> setDefModel, bool isRecursive);
    }
}