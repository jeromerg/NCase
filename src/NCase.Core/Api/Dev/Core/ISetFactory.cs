using System.Collections.Generic;

namespace NCase.Api.Dev.Core
{
    public interface ISetFactory {
        ISet Create(IEnumerable<ICase> cases);
    }
}
