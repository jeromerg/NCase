using NCase.Imp.Nod;

namespace NCase.Api.Nod
{
    public interface ICaseTreeSetNode : ICaseTreeNodeAbstract
    {
        ICaseSet CaseSet { get; }
    }
}