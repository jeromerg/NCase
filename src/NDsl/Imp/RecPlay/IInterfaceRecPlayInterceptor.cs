using NDsl.Util.Castle;

namespace NDsl.Imp.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
        void SetMode(InterfaceRecPlayInterceptor.Mode mode);
    }
}