using NDsl.Api.Core.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseSetNode : IExtendableNode
    {
        ICaseSet CaseSet { get; }
    }
}