using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using Xunit.Sdk;

namespace NCaseFramework.XunitAdapter.Front.Ui
{
    public static class CaseExtensions
    {
        #region inner types

        // ReSharper disable once ClassNeverInstantiated.Local
        private class DummySuccessException : Exception
        {
        }

        #endregion

        public static void ActAndAssert([NotNull, ItemNotNull] this IEnumerable<Case> caseEnumerable, [NotNull] Action<TestCaseContext> actAndAssertAction)
        {
            ActAndAssertShared<DummySuccessException, XunitException>.ActAndAssert(caseEnumerable,
                                                                                   actAndAssertAction,
                                                                                   message => new XunitException(message));
        }
    }
}