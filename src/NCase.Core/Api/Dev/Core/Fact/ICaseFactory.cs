using System.Collections.Generic;
using NCase.Imp;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Core.Fact
{
    public interface IFactFactory
    {
        IFact Create(INode fact);
    }
}
