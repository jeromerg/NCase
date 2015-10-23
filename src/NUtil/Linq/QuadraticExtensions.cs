using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public static class QuadraticExtensions
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<TOut> TriangleUnequal<TIn, TOut>(this IEnumerable<TIn> thiEnumerable, Func<TIn, TIn, TOut> func)
        {
            return thiEnumerable.SelectMany((v1, idx) => thiEnumerable.Skip(idx + 1).Select(v2 => func(v1, v2)));
        }

        public static IEnumerable<TOut> CartesianProduct<TIn1, TIn2, TOut>(this IEnumerable<TIn1> first, IEnumerable<TIn2> second, Func<TIn1, TIn2, TOut> func)
        {
            return from in1 in first 
                   from in2 in second 
                   select func(in1, in2);
        }
    }
}