using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api.Case;
using NCase.Front.Api.CaseEnumerable;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.CaseEnumerable
{
    public class CaseEnumerablefactory : ICaseEnumerableFactory
    {
        [NotNull] private readonly IServices<ICaseEnumerableModel> mServices;
        [NotNull] private readonly ICaseFactory mCaseFactory;

        public CaseEnumerablefactory([NotNull] IServices<ICaseEnumerableModel> services, [NotNull] ICaseFactory caseFactory)
        {
            if (services == null) throw new ArgumentNullException("services");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            mServices = services;
            mCaseFactory = caseFactory;
        }

        public ICaseEnumerable Create(IEnumerable<List<INode>> cases)
        {
            return new CaseEnumerable(cases, mCaseFactory, mServices);
        }
    }
}