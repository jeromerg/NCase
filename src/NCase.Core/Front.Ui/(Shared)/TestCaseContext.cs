using JetBrains.Annotations;

namespace NCaseFramework.Front.Ui
{
    public class TestCaseContext
    {
        private readonly int mTestCaseIndex;
        [NotNull] private readonly Case mCase;

        public TestCaseContext(int testCaseIndex, [NotNull] Case cas)
        {
            mTestCaseIndex = testCaseIndex;
            mCase = cas;
        }

        public int TestCaseIndex
        {
            get { return mTestCaseIndex; }
        }

        [NotNull] public Case Case
        {
            get { return mCase; }
        }

        [CanBeNull] public ExceptionAssert ExceptionAssert { get; set; }
    }
}