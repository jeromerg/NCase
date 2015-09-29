using NDsl.Api.Core;

namespace NDsl.Api.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        string ContributorName { get; }
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
        void SetMode(RecPlayMode mode);
    }
}