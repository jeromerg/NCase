using NCase.Front.Api;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Front.Imp
{
    public interface IFactFactory
    {
        IFact Create(INode fact);
    }
}