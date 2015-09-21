using System.Collections.Generic;
using NCase.Api.Pub;

namespace NCase.Api.Dev.Core
{
    public interface ISetFactory {
        ISet Create(IEnumerable<ICase> cases);
    }
}
