using System;
using System.Collections.Generic;
using NCaseFramework.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    public static class CaseExtensions
    {
        public static void ActAndAssert(this IEnumerable<Case> caseEnumerable, Action<TestCaseContext> actAndAssertAction)
        {
            ActAndAssertShared<SuccessException, AssertionException>.ActAndAssert(caseEnumerable,
                                                                                  actAndAssertAction,
                                                                                  message => new AssertionException(message));
        }
    }
}