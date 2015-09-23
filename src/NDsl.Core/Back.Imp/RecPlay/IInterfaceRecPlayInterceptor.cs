using NDsl.Back.Api.Core;

namespace NDsl.Back.Imp.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
        void SetMode(InterfaceRecPlayInterceptor.Mode mode);
    }
}