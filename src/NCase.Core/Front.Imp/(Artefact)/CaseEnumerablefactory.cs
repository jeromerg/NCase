using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
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