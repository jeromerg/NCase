using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Api.RecPlay
{
    public interface IInterfaceRecPlayNode : INode
    {
        [CanBeNull] object PropertyValue { get; }
        [NotNull] PropertyCallKey PropertyCallKey { get; }
        [NotNull] string ContributorName { get; }

        void SetReplay(bool value);
    }
}