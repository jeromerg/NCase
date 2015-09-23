using NCase.All;

namespace NCase.Front.Imp
{
    public interface IAdvanced<out T>
    {
        IResolver Resolver { get; }
        T Root { get; }
    }
}