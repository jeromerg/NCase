using NDsl.Util.Castle;

namespace NDsl.Core.InterfaceContrib
{
    public interface IReplayInterceptor
    {
        void AddReplayValue(PropertyCallKey callKey, object value);
    }
}