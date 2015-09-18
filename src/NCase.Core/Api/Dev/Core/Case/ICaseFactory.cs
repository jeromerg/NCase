using System.Collections.Generic;
using NCase.Imp;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Core.Case
{
    public interface ICaseFactory
    {
        ICase Create(IEnumerable<INode> facts);
    }
}
