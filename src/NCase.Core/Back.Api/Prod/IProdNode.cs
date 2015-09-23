using NCase.Back.Api.Core;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Back.Api.Prod
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : IDefNode
    {
        void AddChild(INode child);
    }
}