using NDsl.Util.Castle;

namespace NDsl.Imp.RecPlay
{
    public interface IRecPlayInterfaceInterceptor
    {
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
    }
}