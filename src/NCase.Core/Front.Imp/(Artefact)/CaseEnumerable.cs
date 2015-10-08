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
        [NotNull] private readonly ICaseFactory mCaseFactory;


        public class Factory : ICaseEnumerableFactory
        {
            [NotNull] private readonly IToolBox<ICaseEnumerableApi> mToolBox;
            [NotNull] private readonly ICaseFactory mCaseFactory;

            public Factory([NotNull] IToolBox<ICaseEnumerableApi> toolBox, [NotNull] ICaseFactory caseFactory)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                if (caseFactory == null) throw new ArgumentNullException("caseFactory");
                mToolBox = toolBox;
                mCaseFactory = caseFactory;
            }

            public ICaseEnumerable Create(IEnumerable<List<INode>> cases)
            {
                return new CaseEnumerable(cases, mCaseFactory, mToolBox);
            }
        }

        public CaseEnumerable([NotNull] IEnumerable<List<INode>> cases,
                              [NotNull] ICaseFactory caseFactory,
                              [NotNull] IToolBox<ICaseEnumerableApi> toolBox)
            : base(toolBox)
        {
            if (cases == null) throw new ArgumentNullException("cases");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mCases = cases;
            mCaseFactory = caseFactory;
        }

        public IEnumerable<List<INode>> Cases
        {
            get { return mCases; }
        }

        protected override ICaseEnumerableApi GetApi()
        {
            return this;
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