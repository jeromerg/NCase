using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Prod
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdDimNode : INode
    {
        [NotNull] INode FirstChild { get; }
        void PlaceNextValue([NotNull] INode child);
    }
}