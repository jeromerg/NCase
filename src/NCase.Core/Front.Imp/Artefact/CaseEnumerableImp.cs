using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Artefact
{
    public class CaseEnumerableImp : ArtefactImpBase<ICaseEnumerable, CaseEnumerableImp>, ICaseEnumerable
    {
        #region inner types

        public class Factory
        {
            [NotNull] private readonly IOperationDirector mOperationDirector;
            [NotNull] private readonly CaseImp.Factory mCaseFactory;

            public Factory([NotNull] IOperationDirector operationDirector, [NotNull] CaseImp.Factory caseFactory)
            {
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                if (caseFactory == null) throw new ArgumentNullException("caseFactory");
                mOperationDirector = operationDirector;
                mCaseFactory = caseFactory;
            }

            public virtual ICaseEnumerable Create(IEnumerable<List<INode>> cases)
            {
                return new CaseEnumerableImp(cases, mOperationDirector, mCaseFactory);
            }
        }

        #endregion

        private readonly IEnumerable<List<INode>> mCases;
        private readonly CaseImp.Factory mCaseFactory;

        public CaseEnumerableImp(IEnumerable<List<INode>> cases, IOperationDirector operationDirector, CaseImp.Factory caseFactory)
            : base(operationDirector)
        {
            mCases = cases;
            mCaseFactory = caseFactory;
        }


        protected override CaseEnumerableImp ThisDefImpl
        {
            get { return this; }
        }

        public IEnumerable<List<INode>> Cases
        {
            get { return mCases; }
        }

        #region

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