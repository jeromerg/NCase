using NDsl.Util.Castle;

namespace NDsl.Impl.RecPlay
{
    public interface IRecPlayInterfaceInterceptor
    {
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
    }
}