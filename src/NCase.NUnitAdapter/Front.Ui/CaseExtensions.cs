using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    public static class CaseExtensions
    {
        public static void ActAndAssert([NotNull, ItemNotNull] this IEnumerable<Case> caseEnumerable,
                                        [NotNull] Action<TestCaseContext> actAndAssertAction)
        {
            if (caseEnumerable == null) throw new ArgumentNullException("caseEnumerable");
            if (actAndAssertAction == null) throw new ArgumentNullException("actAndAssertAction");

            ActAndAssertShared<SuccessException, AssertionException>.ActAndAssert(caseEnumerable,
                                                                                  actAndAssertAction,
                                                                                  message => new AssertionException(message));
        }
    }
}