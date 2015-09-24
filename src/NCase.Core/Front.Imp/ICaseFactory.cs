using System.Collections.Generic;
using NCase.Front.Api;
using NDsl.Api.Core;

namespace NCase.Front.Imp
{
    public interface ICaseFactory
    {
        ICase Create(IEnumerable<INode> facts);
    }
}