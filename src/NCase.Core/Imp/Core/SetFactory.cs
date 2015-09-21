using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;

namespace NCase.Imp.Core
{
    public class SetFactory : ISetFactory
    {
        public ISet Create(IEnumerable<ICase> cases)
        {
            return new Set(cases);
        }
    }
}
