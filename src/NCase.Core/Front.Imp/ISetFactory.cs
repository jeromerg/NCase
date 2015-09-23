using System.Collections.Generic;
using NCase.Front.Api;

namespace NCase.Front.Imp
{
    public interface ISetFactory
    {
        ISet Create(IEnumerable<ICase> cases);
    }
}