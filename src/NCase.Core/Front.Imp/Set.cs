using System.Collections;
using System.Collections.Generic;
using NCase.Front.Api;

namespace NCase.Front.Imp
{
    public class Set : ISet
    {
        #region inner types

        public class Factory : ISetFactory
        {
            public ISet Create(IEnumerable<ICase> cases)
            {
                return new Set(cases);
            }
        }

        #endregion

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