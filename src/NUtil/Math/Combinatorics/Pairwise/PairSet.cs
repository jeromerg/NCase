using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NUtil.Linq;

namespace NUtil.Math.Combinatorics.Pairwise
{
    public class PairSet
    {
        [NotNull] private readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> mPairs
            = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();

        public PairSet()
        {
        }

        public PairSet([NotNull] IList<int> dimSizes)
        {
            if (dimSizes == null) throw new ArgumentNullException("dimSizes");

            // fill in with all pairs between all dimensions
            for (int dim1 = 0; dim1 < dimSizes.Count; dim1++)
            {
                int dimSize1 = dimSizes[dim1];

                for (int dim2 = dim1 + 1; dim2 < dimSizes.Count; dim2++)
                {
                    int dimSize2 = dimSizes[dim2];

                    foreach (int val1 in Enumerable.Range(0, dimSize1))
                        foreach (int val2 in Enumerable.Range(0, dimSize2))
                            Add(dim1, val1, dim2, val2);
                }
            }
        }

        public bool Any()
        {
            return mPairs.Any();
        }

        public void Add([NotNull] Pair pair)
        {
            if (pair == null) throw new ArgumentNullException("pair");

            Add(pair.Dim1, pair.Val1, pair.Dim2, pair.Val2);
        }

        public void Add(int dim1, int val1, int dim2, int val2)
        {
            // ReSharper disable PossibleNullReferenceException

            mPairs
                .CascadeAdd(dim1)
                .CascadeAdd(dim2)
                .CascadeAdd(val1)
                .Add(val2);

            mPairs
                .CascadeAdd(dim2)
                .CascadeAdd(dim1)
                .CascadeAdd(val2)
                .Add(val1);

            // ReSharper restore PossibleNullReferenceException
        }

        [CanBeNull]
        public Pair FirstOrDefault(int dim1Constraint, int dim2Constraint)
        {
            int val1, val2;
            bool ok = mPairs
                .CascadeGetOrDefault(dim1Constraint)
                .CascadeGetOrDefault(dim2Constraint)
                .CascadeTryFirst(out val1)
                .CascadeTryFirst(out val2);

            return ok
                       ? new Pair(dim1Constraint, val1, dim2Constraint, val2)
                       : null;
        }

        public Pair FirstOrDefault(int dim1Constraint, int val1Constraint, int dim2Constraint)
        {
            int val2;
            bool ok = mPairs
                .CascadeGetOrDefault(dim1Constraint)
                .CascadeGetOrDefault(dim2Constraint)
                .CascadeGetOrDefault(val1Constraint)
                .CascadeTryFirst(out val2);

            return ok
                       ? new Pair(dim1Constraint, val1Constraint, dim2Constraint, val2)
                       : null;
        }

        public void Remove([NotNull] Pair pair)
        {
            if (pair == null) throw new ArgumentNullException("pair");
            Remove(pair.Dim1, pair.Val1, pair.Dim2, pair.Val2);
        }

        public void Remove(int dim1, int val1, int dim2, int val2)
        {
            // ReSharper disable AssignNullToNotNullAttribute
            mPairs.CascadeRemove(dim1).CascadeRemove(dim2).CascadeRemove(val1).CascadeRemove(val2);
            mPairs.CascadeRemove(dim2).CascadeRemove(dim1).CascadeRemove(val2).CascadeRemove(val1);
            // ReSharper restore AssignNullToNotNullAttribute            
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            int count = 0;

            // ReSharper disable PossibleNullReferenceException
            foreach (KeyValuePair<int, Dictionary<int, Dictionary<int, HashSet<int>>>> dim1 in mPairs)
                foreach (KeyValuePair<int, Dictionary<int, HashSet<int>>> dim2 in dim1.Value)
                    foreach (KeyValuePair<int, HashSet<int>> val1 in dim2.Value)
                        foreach (int val2 in val1.Value)
                        {
                            count++;
                            sb.AppendFormat("({0},{1};{2},{3})\n", dim1.Key, val1.Key, dim2.Key, val2);
                        }
            // ReSharper restore PossibleNullReferenceException

            return string.Format("Count:{0}, List:{1}", count, sb);
        }

        public int GetCount()
        {
            int count = 0;

            // ReSharper disable PossibleNullReferenceException
            foreach (KeyValuePair<int, Dictionary<int, Dictionary<int, HashSet<int>>>> dim1 in mPairs)
                foreach (KeyValuePair<int, Dictionary<int, HashSet<int>>> dim2 in dim1.Value)
                    foreach (KeyValuePair<int, HashSet<int>> val1 in dim2.Value)
                        count += val1.Value.Count;
            // ReSharper restore PossibleNullReferenceException

            return count;
        }
    }
}