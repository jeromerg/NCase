using System.Diagnostics.CodeAnalysis;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface CombinationsDefiner : Definer
    {
        void Child();
    }
}