using NDsl.Front.Api;

namespace NCase.Front.Api
{
    public interface ISetDef : IDef
    {
    }

    public interface ISetDef<out TSetDef> : IDef<TSetDef>, ISetDef
        where TSetDef : ISetDef<TSetDef>
    {
    }
}