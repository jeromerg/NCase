using NCase.Api.Dev.Core;

namespace NCase.Api.Pub
{
    public interface IAdvanced<out T>
    {
        IResolver Resolver { get; }
        T Root { get; }
    }
}