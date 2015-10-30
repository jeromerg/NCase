using System;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Api;
using NUtil.Generics;

namespace NCaseFramework.NunitAdapter.Front.Ui
{

    public static class CaseEnumerableExtensions
    {
        public static CaseEnumerable ActAndAssert(
            this CaseEnumerable caseEnumerable,
            Action<Holder<ExceptionAssert>> actAndAssertAction)
        {
            var actAndAssert = caseEnumerable.Api.Services.GetService<IActAndAssert>();
            return actAndAssert.Perform(caseEnumerable, actAndAssertAction);
        }
    }
}