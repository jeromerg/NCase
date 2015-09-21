using System.Collections;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Pub;

namespace NCase.Imp.Core
{
    public class Set : ISet
    {
        private readonly IEnumerable<ICase> mCases;

        public Set(IEnumerable<ICase> cases)
        {
            mCases = cases;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ICase> GetEnumerator()
        {
            return mCases.GetEnumerator();
        }
    }
}
