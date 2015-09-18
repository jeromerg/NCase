using System.Collections.Generic;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Core
{
    public interface ICaseFactory
    {
        ICase Create(IEnumerable<INode> facts);
    }
}
