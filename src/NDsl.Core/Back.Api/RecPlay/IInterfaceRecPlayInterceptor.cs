using JetBrains.Annotations;
using NDsl.Back.Api.Ex;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayInterceptor
    {
        [NotNull] string ContributorName { get; }

        /// <exception cref="InvalidRecPlayStateException" accessor="set">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and
        ///     cannot be set twice
        /// </exception>
        void AddReplayPropertyValue([NotNull] PropertyCallKey callKey, [CanBeNull] object value);

        /// <exception cref="InvalidRecPlayStateException" accessor="set">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and
        ///     cannot be set twice
        /// </exception>
        void RemoveReplayPropertyValue([NotNull] PropertyCallKey callKey);
    }
}