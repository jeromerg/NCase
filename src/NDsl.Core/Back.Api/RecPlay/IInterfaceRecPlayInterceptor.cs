using NDsl.Back.Api.Ex;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        string ContributorName { get; }

        /// <exception cref="InvalidRecPlayStateException" accessor="set">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and
        ///     cannot be set twice
        /// </exception>
        void AddReplayPropertyValue(PropertyCallKey callKey, object value);

        /// <exception cref="InvalidRecPlayStateException" accessor="set">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and
        ///     cannot be set twice
        /// </exception>
        void RemoveReplayPropertyValue(PropertyCallKey callKey);
    }
}