using System;
using System.Collections.Generic;
using NCaseFramework.Front.Ui;
using Xunit.Sdk;

namespace NCaseFramework.xUnitAdapter.Front.Ui
{
    public static class CaseExtensions
    {
        #region inner types

        // ReSharper disable once ClassNeverInstantiated.Local
        private class DummySuccessException : Exception
        {
        }

        #endregion

        public static void ActAndAssert(this IEnumerable<Case> caseEnumerable, Action<TestCaseContext> actAndAssertAction)
        {
            ActAndAssertShared<DummySuccessException, XunitException>.ActAndAssert(caseEnumerable,
                                                                                   actAndAssertAction,
                                                                                   message => new XunitException(message));
        }
    }
}