using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CaseEnumerableImp : ArtefactImp<ICaseEnumerableModel>, CaseEnumerable, ICaseEnumerableModel
    {
        [NotNull] private readonly IEnumerable<List<INode>> mCases;
        [NotNull] private readonly ICaseFactory mCaseFactory;

        public class Factory : ICaseEnumerableFactory
        {
            [NotNull] private readonly IServiceSet<ICaseEnumerableModel> mServices;
            [NotNull] private readonly ICaseFactory mCaseFactory;

            public Factory([NotNull] IServiceSet<ICaseEnumerableModel> services, [NotNull] ICaseFactory caseFactory)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (caseFactory == null) throw new ArgumentNullException("caseFactory");
                mServices = services;
                mCaseFactory = caseFactory;
            }

            public CaseEnumerable Create(IEnumerable<List<INode>> cases)
            {
                return new CaseEnumerableImp(cases, mCaseFactory, mServices);
            }
        }

        public CaseEnumerableImp([NotNull] IEnumerable<List<INode>> cases,
                                 [NotNull] ICaseFactory caseFactory,
                                 [NotNull] IServiceSet<ICaseEnumerableModel> services)
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

        public IEnumerator<Case> GetEnumerator()
        {
            return Cases
                .Select(cas => mCaseFactory.Create(cas))
                .GetEnumerator();
        }

        #endregion
    }
}