using NDsl.Api.Core;

namespace NCase.Api.Nod
{
    public interface ICaseSetNode : IExtendableNode
    {
        CaseSet CaseSetName { get; }
    }
}