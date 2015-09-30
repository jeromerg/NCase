using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ISetDef : IDef
    {
    }

    public interface ISetDef<out TSetDef> : IDef<TSetDef>, ISetDef
        where TSetDef : ISetDef<TSetDef>
    {
    }
}