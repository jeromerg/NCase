using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NUtil.Linq
{
    public static class LinqForeachExtensions
    {

        public static void ForEach<T>([NotNull] this IEnumerable<T> thisEnumerable, [NotNull] Action<T> func)
        {
            if (thisEnumerable == null) throw new ArgumentNullException("thisEnumerable");
            if (func == null) throw new ArgumentNullException("func");

            foreach (T val in thisEnumerable)
                func(val);
        }

        public static void ForEach<T>([NotNull] this IEnumerable<T> thisEnumerable,
                                           [NotNull] Action<T> action,
                                           [NotNull] Action actionBetweenEachItem)
        {
            if (thisEnumerable == null) throw new ArgumentNullException("thisEnumerable");
            if (action == null) throw new ArgumentNullException("action");
            if (actionBetweenEachItem == null) throw new ArgumentNullException("actionBetweenEachItem");

            bool isFirst = true;
            foreach (T val in thisEnumerable)
            {
                if(!isFirst)
                    actionBetweenEachItem();

                isFirst = false;

                action(val);
            }
        }
    }
}