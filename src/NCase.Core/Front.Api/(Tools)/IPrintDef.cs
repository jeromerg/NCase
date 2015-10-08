using NCase.Front.Api;
using NCase.Front.Ui;

namespace NCase.Front.Imp.Op
{
    public interface IPrintDef
    {
        ICaseEnumerable Perform(ISetDefApi setDefApi);
    }
}