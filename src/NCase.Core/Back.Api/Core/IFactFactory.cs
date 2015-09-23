using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Core
{
    public interface IFactFactory
    {
        IFact Create(INode fact);
    }
}