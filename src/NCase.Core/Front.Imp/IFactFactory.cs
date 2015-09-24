using NCase.Front.Api;
using NDsl.Api.Core;

namespace NCase.Front.Imp
{
    public interface IFactFactory
    {
        IFact Create(INode fact);
    }
}