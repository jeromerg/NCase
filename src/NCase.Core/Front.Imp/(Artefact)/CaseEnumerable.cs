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
    public class CaseEnumerable : Artefact<ICaseEnumerableModel>, ICaseEnumerable, ICaseEnumerableModel
    {
        [NotNull] private readonly IEnumerable<List<INode>> mCases;
        [NotNull] private readonly ICaseFactory mCaseFactory;


        public CaseEnumerable([NotNull] IEnumerable<List<INode>> cases,
                              [NotNull] ICaseFactory caseFactory,
                              [NotNull] IServices<ICaseEnumerableModel> services)
            : base(services)
        {
            if (cases == null) throw new ArgumentNullException("cases");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mCases = cases;
            mCaseFactory = caseFactory;
        }

        public override ICaseEnumerableModel Model
        {
            get { return this; }
        }

        #region ICaseEnumerableModel

        public IEnumerable<List<INode>> Cases
        {
            get { return mCases; }
        }

        #endregion

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