using NCaseFramework.Front.Ui;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    public class TestCaseContext
    {
        private readonly int mTestCaseIndex;
        private readonly Case mCase;        
        private ExceptionAssert mExceptionAssert;

        public TestCaseContext(int testCaseIndex, Case cas)
        {
            mTestCaseIndex = testCaseIndex;
            mCase = cas;
        }

        public int TestCaseIndex
        {
            get { return mTestCaseIndex; }
        }

        public Case Case
        {
            get { return mCase; }
        }

        public ExceptionAssert ExceptionAssert
        {
            get { return mExceptionAssert; }
            set { mExceptionAssert = value; }
        }
    }
}