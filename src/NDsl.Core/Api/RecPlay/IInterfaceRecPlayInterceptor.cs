using NDsl.Api.Core;
using NDsl.Imp.RecPlay;

namespace NDsl.Api.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
        void SetMode(RecPlayMode mode);
        string ContributorName { get; }
    }
}