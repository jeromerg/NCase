using NCase.Util.Castle;

namespace NCase.Core.InterfaceContrib
{
    public interface IReplayInterceptor
    {
        void Replay(PropertyCallKey propertyInfo, object value);
    }
}