using NCase.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IAdvanced<out T>
    {
        IResolver Resolver { get; }
        T Root { get; }
    }
}