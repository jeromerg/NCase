
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class CaseEnumerable : Artefact<ICaseEnumerableApi>, ICaseEnumerable, ICaseEnumerableApi
    {
        [NotNull] private readonly IEnumerable<List<INode>> mCases;
        [NotNull] private readonly CaseImp.Factory mCaseFactory;

        public CaseEnumerable([NotNull] IEnumerable<List<INode>> cases,
                                 [NotNull] CaseImp.Factory caseFactory, 
            ITools tools)
            : base(tools)
        {
            if (cases == null) throw new ArgumentNullException("cases");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mCases = cases;
            mCaseFactory = caseFactory;
        }

        public override ICaseEnumerableApi Api
        {
            get { return this; }
        }

        public IEnumerable<List<INode>> Cases
        {
            get { return mCases; }
        }

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ICase> GetEnumerator()
        {
            foreach (List<INode> @case in Cases)
                yield return mCaseFactory.Create(@case);
        }

        #endregion
    }
}