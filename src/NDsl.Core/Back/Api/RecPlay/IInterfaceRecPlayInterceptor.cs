using NDsl.Back.Api.Core;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        string ContributorName { get; }
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
        void SetMode(RecPlayMode mode);
    }
}