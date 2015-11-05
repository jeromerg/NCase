using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.SetDef;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Api;
using NCaseFramework.NunitAdapter.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.NunitAdapter.Front.Imp
{
    public class ActAndAssert : IActAndAssert
    {
        private readonly IPrintCase mPrintCase;

        public ActAndAssert(IPrintCase printCase)
        {
            mPrintCase = printCase;
        }

        public void Perform(CaseEnumerable caseEnumerable, Action<TestCaseContext> actAndAssertAction)
        {
            PerformImp(caseEnumerable, actAndAssertAction);
        }

        public void PerformImp(CaseEnumerable caseEnumerable, Action<TestCaseContext> actAndAssertAction)
        {
            var results = new List<Exception>();

            int caseIndex = 0;
            foreach (Case cas in caseEnumerable)
            {
                PerformCaseTest(actAndAssertAction, caseIndex, cas, results);
                caseIndex++;
            }

            if (results.All(result => result == null))
                return; // NO ERROR

            // CASE SOME ERROR:

            IEnumerable<string> errorStrings = results
                .Select((errorIfAny, index) => new {errorIfAny, index})
                .Where(r => r.errorIfAny != null)
                .Select(r => CreateErrorText(r.errorIfAny, false));

            string msg = string.Format("Following test cases failed:\n{0}", string.Join("\n", errorStrings));
            throw new AssertionException(msg);
        }

        private void PerformCaseTest(Action<TestCaseContext> actAndAssertAction, int caseIndex, Case cas, List<Exception> results)
        {
            Console.WriteLine();
            Console.WriteLine("Test Case #{0}", caseIndex);
            Console.WriteLine("================");
            Console.WriteLine();
            Console.WriteLine("Definition");
            Console.WriteLine("----------");
            Console.WriteLine();
            Console.WriteLine(mPrintCase.Perform(cas.Zapi.Model));
            Console.WriteLine();
            Console.WriteLine("Act and Assert");
            Console.WriteLine("--------------");

            var testCaseContext = new TestCaseContext(caseIndex, cas);
            try
            {
                actAndAssertAction(testCaseContext);
            }
            catch (OperationCanceledException e)
            {
                PrintResultAndAddStore(results, e);
                throw;
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
                if (testCaseContext.ExceptionAssert != null &&
                    testCaseContext.ExceptionAssert.IsExpectedExceptionPredicate(e))
                {
                    PrintResultAndAddStore(results, null);
                }
                else if (testCaseContext.ExceptionAssert != null)
                {
                    string message = string.Format("Expected an exception {0}, but thrown exception was {1}",
                                                   testCaseContext.ExceptionAssert.Description,
                                                   e);
                    PrintResultAndAddStore(results, new AssertionException(message));
                }
                else
                {
                    string message = string.Format("No exception expected, but following exception was thrown: {0}", e);
                    PrintResultAndAddStore(results, new AssertionException(message));
                }
            }

            if (testCaseContext.ExceptionAssert != null)
            {
                string message = string.Format("Expected an exception {0}, but not exception was thrown",
                                               testCaseContext.ExceptionAssert.Description);
                PrintResultAndAddStore(results, new AssertionException(message));
            }
            else
            {
                PrintResultAndAddStore(results, null);
            }
        }

        private void PrintResultAndAddStore(List<Exception> results, [CanBeNull] Exception errorIfAny)
        {
            results.Add(errorIfAny);
            PrintResult(errorIfAny, true);
        }

        private static void PrintResult(Exception error, bool printErrorDetails)
        {
            string resultText = error == null
                                    ? CreateSuccessText()
                                    : CreateErrorText(error, printErrorDetails);

            Console.WriteLine();
            Console.WriteLine(resultText);
            Console.WriteLine();
        }

        private static string CreateSuccessText()
        {
            return "TEST CASE RESULT: SUCCESSFUL!";
        }

        private static string CreateErrorText(Exception error, bool printErrorDetails)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("TEST CASE RESULT: ERROR {0}", error.Message));
            if (printErrorDetails)
                sb.AppendLine(string.Format("Error Details: {0}", error));

            return sb.ToString();
        }
    }
}