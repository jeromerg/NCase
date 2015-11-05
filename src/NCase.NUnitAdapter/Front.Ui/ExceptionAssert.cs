using System;
using JetBrains.Annotations;

namespace NCaseFramework.NunitAdapter.Front.Ui
{
    /// <summary>Exception assertion</summary>
    public class ExceptionAssert
    {
        private readonly Predicate<Exception> mIsExpectedExceptionPredicate;
        private readonly string mDescription;

        public ExceptionAssert(Predicate<Exception> isExpectedExceptionPredicate, string description)
        {
            mIsExpectedExceptionPredicate = isExpectedExceptionPredicate;
            mDescription = description;
        }

        public static ExceptionAssert IsAssignableTo<T>() where T : Exception
        {
            return IsAssignableTo(typeof (T));
        }

        public static ExceptionAssert IsAssignableTo([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            string description = string.Format("assignable to '{0}'", type.FullName);
            return new ExceptionAssert(e => type.IsInstanceOfType(e), description);
        }

        public static ExceptionAssert IsOfType<T>() where T : Exception
        {
            return IsOfType(typeof (T));
        }

        public static ExceptionAssert IsOfType(Type type)
        {
            string description = string.Format("of type '{0}'", type.FullName);
            return new ExceptionAssert(e => e.GetType() == type, description);
        }

        public Predicate<Exception> IsExpectedExceptionPredicate
        {
            get { return mIsExpectedExceptionPredicate; }
        }

        public string Description
        {
            get { return mDescription; }
        }
    }
}