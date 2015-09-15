using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Prod
{
    /// <summary>
    /// A child corresponds to a set of cases, which will be used in the cartesian product
    /// </summary>
    public interface IProdNode : ICaseSetNode
    {
        void AddChild(INode child);
    }
}