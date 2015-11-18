using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace NUtil.Linq
{
    public static class QuadraticExtensions
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [NotNull] 
        public static IEnumerable<TOut> TriangleUnequal<TIn, TOut>([NotNull] this IEnumerable<TIn> thisEnumerable,
                                                                   [NotNull] Func<TIn, TIn, TOut> func)
        {
            if (thisEnumerable == null) throw new ArgumentNullException("thisEnumerable");
            if (func == null) throw new ArgumentNullException("func");

            return thisEnumerable.SelectMany((v1, idx) => thisEnumerable.Skip(idx + 1).Select(v2 => func(v1, v2)));
        }

        [NotNull] 
        public static IEnumerable<TOut> CartesianProduct<TIn1, TIn2, TOut>([NotNull] this IEnumerable<TIn1> first,
                                                                           [NotNull] IEnumerable<TIn2> second,
                                                                           [NotNull] Func<TIn1, TIn2, TOut> func)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (func == null) throw new ArgumentNullException("func");

            // ReSharper disable once PossibleMultipleEnumeration
            return from in1 in first
                   from in2 in second
                   select func(in1, in2);
        }
    }
}