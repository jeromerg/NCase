using System;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Api;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    public static class CaseEnumerableExtensions
    {
        public static void ActAndAssert(
            this CaseEnumerable caseEnumerable,
            Action<TestCaseContext> actAndAssertAction)
        {
            var actAndAssert = caseEnumerable.Zapi.Services.GetService<IActAndAssert>();
            actAndAssert.Perform(caseEnumerable, actAndAssertAction);
        }
    }
}