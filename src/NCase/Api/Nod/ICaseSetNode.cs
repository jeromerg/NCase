using NDsl.Api.Core.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseSetNode : ITreeCaseSetNode
    {
        ICaseSet CaseSet { get; }
    }
}