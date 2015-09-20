using NCase.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Prod
{
    /// <summary>
    /// A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : IDefNode
    {
        void AddChild(INode child);
    }
}