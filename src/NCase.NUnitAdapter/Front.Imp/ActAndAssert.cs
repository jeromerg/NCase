using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Api;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Back.Api.Common;
using NUnit.Framework;
using NUtil.Generics;

namespace NCaseFramework.NunitAdapter.Front.Imp
{
    public class ActAndAssert : IActAndAssert
    {
        private readonly IPrintCase mPrintCase;
        private readonly ICaseEnumerableFactory mCaseEnumerableFactory;

        public ActAndAssert(IPrintCase printCase, ICaseEnumerableFactory caseEnumerableFactory)
        {
            mPrintCase = printCase;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public CaseEnumerable Perform(CaseEnumerable caseEnumerable, Action<Holder<ExceptionAssert>> actAndAssertAction)
        {
            IEnumerable<List<INode>> cases = PerformImp(caseEnumerable, actAndAssertAction);
            return mCaseEnumerableFactory.Create(cases);
        }

        public IEnumerable<List<INode>> PerformImp(CaseEnumerable caseEnumerable, Action<Holder<ExceptionAssert>> actAndAssertAction)
        {
            var results = new List<Exception>();

            int caseIndex = 0;
            foreach (Case cas in caseEnumerable)
            {
                Console.WriteLine("Case #{0,5}:", caseIndex);
                Console.WriteLine("============");
                Console.WriteLine();
                Console.WriteLine("Definition");
                Console.WriteLine("----------");
                Console.WriteLine();
                Console.WriteLine(mPrintCase.Perform(cas.Zapi.Model));
                Console.WriteLine();
                Console.WriteLine("Act and Assert");
                Console.WriteLine("--------------");

                var exceptionAsserter = new Holder<ExceptionAssert>();
                try
                {
                    actAndAssertAction(exceptionAsserter);
                }
                catch (SuccessException)
                {
                    PrintResultAndAddStore(results, null);
                }
                catch (AssertionException e)
                {
                    PrintResultAndAddStore(results, e);
                }
                catch (Exception e)
                {
                    if(exceptionAsserter.Value != null && 
                       exceptionAsserter.Value.IsExpectedExceptionPredicate(e))
                    {
                        PrintResultAndAddStore(results, null);
                    }
                    else 
                    {
                        PrintResultAndAddStore(results, e);
                    }
                }
                caseIndex++;
                yield return cas.Zapi.Model.FactNodes;
            }

            if (results.All(result => result == null))
                yield break;

            var errorStrings = results
                .Select((errorIfAny, index) => new { errorIfAny, index })
                .Where(r => r.errorIfAny != null)
                .Select(r => PrintError(r.index, r.errorIfAny, false));

            string msg = string.Format("Following test cases failed:\n{0}", string.Join("\n", errorStrings));
            throw new AssertionException(msg);
        }

        private void PrintResultAndAddStore(List<Exception> results, [CanBeNull] Exception errorIfAny)
        {
            results.Add(errorIfAny);
            PrintResult(results.Count-1, errorIfAny, true);
        }

        private static void PrintResult(int caseIndex, Exception error, bool printErrorDetails)
        {
            string msg = error == null 
                ? PrintSuccess(caseIndex) 
                : PrintError(caseIndex, error, printErrorDetails);

            Console.WriteLine(msg);
        }

        private static string PrintSuccess(int caseIndex)
        {
            return string.Format("Case #{0}: SUCCESS", caseIndex);
        }

        private static string PrintError(int caseIndex, Exception error, bool printErrorDetails)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Case #{0}: ERROR {1}", caseIndex, error.Message));
            if (printErrorDetails)
                sb.AppendLine(string.Format("Error Details: {0}", error));

            return sb.ToString();
        }
    }
}