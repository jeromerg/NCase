using System;
using JetBrains.Annotations;

namespace NCaseFramework.Front.Ui
{
    /// <summary>Exception assertion</summary>
    public class ExceptionAssert
    {
        [NotNull] private readonly Predicate<Exception> mIsExpectedExceptionPredicate;
        [NotNull] private readonly string mDescription;

        public ExceptionAssert([NotNull] Predicate<Exception> isExpectedExceptionPredicate, [NotNull] string description)
        {
            if (isExpectedExceptionPredicate == null) throw new ArgumentNullException("isExpectedExceptionPredicate");
            if (description == null) throw new ArgumentNullException("description");

            mIsExpectedExceptionPredicate = isExpectedExceptionPredicate;
            mDescription = description;
        }

        [NotNull]
        public static ExceptionAssert IsAssignableTo<T>() where T : Exception
        {
            return IsAssignableTo(typeof (T));
        }

        [NotNull]
        public static ExceptionAssert IsAssignableTo([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            string description = string.Format("assignable to '{0}'", type.FullName);
            return new ExceptionAssert(e => type.IsInstanceOfType(e), description);
        }

        [NotNull]
        public static ExceptionAssert IsOfType<T>() where T : Exception
        {
            return IsOfType(typeof (T));
        }

        [NotNull]
        public static ExceptionAssert IsOfType([NotNull] Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            string description = string.Format("of type '{0}'", type.FullName);
            // ReSharper disable once PossibleNullReferenceException
            return new ExceptionAssert(e => e.GetType() == type, description);
        }

        [NotNull] public Predicate<Exception> IsExpectedExceptionPredicate
        {
            get { return mIsExpectedExceptionPredicate; }
        }

        [NotNull] public string Description
        {
            get { return mDescription; }
        }
    }
}