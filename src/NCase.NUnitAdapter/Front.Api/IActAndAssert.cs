using System;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Back.Api.Util;
using NUtil.Generics;

namespace NCaseFramework.NunitAdapter.Front.Api
{
    public interface IActAndAssert : IService<ICaseEnumerableModel>
    {
        CaseEnumerable Perform(CaseEnumerable caseEnumerable, Action<TestCaseContext> actAndAssertAction);
    }
}