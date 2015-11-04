using System;
using JetBrains.Annotations;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    /// <summary>Exception assertion</summary>
    public class ExceptionAssert
    {
        private readonly Predicate<Exception> mIsExpectedExceptionPredicate;

        public ExceptionAssert(Predicate<Exception> isExpectedExceptionPredicate)
        {
            mIsExpectedExceptionPredicate = isExpectedExceptionPredicate;
        }

        public static ExceptionAssert IsAssignableTo<T>() where T : Exception
        {
            return IsAssignableTo(typeof(T));
        }

        public static ExceptionAssert IsAssignableTo([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return new ExceptionAssert(e => type.IsInstanceOfType(e));
        }

        public static ExceptionAssert IsOfType<T>() where T : Exception
        {
            return IsOfType(typeof(T));
        }
        
        public static ExceptionAssert IsOfType(Type type)
        {
            return new ExceptionAssert(e => e.GetType() == type);
        }

        public Predicate<Exception> IsExpectedExceptionPredicate
        {
            get { return mIsExpectedExceptionPredicate; }
        }
    }
}