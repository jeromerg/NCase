using NCase.Front.Api;
using NCase.Front.Ui;

namespace NCase.Front.Imp.Op
{
    public interface IPrintTable
    {
        ICaseEnumerable Perform(ISetDefApi setDefApi);
    }
}