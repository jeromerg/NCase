using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NCase.Back.Imp.Pairwise
{
    public class PairSet
    {
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> mPairs
            = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();

        public PairSet()
        {
        }

        public PairSet(IList<int> dimSizes)
        {
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

        public void Add(Pair pair)
        {
            Add(pair.Dim1, pair.Val1, pair.Dim2, pair.Val2);
        }

        public void Add(int dim1, int val1, int dim2, int val2)
        {
            mPairs
                .CascadeAdd(dim1)
                .CascadeAdd(dim2)
                .CascadeAdd(val1)
                .CascadeAdd(val2);

            mPairs
                .CascadeAdd(dim2)
                .CascadeAdd(dim1)
                .CascadeAdd(val2)
                .CascadeAdd(val1);
        }

        [CanBeNull]
        public Pair FirstOrDefault(int dim1Constraint, int dim2Constraint)
        {
            int val1, val2;
            bool ok = mPairs
                .CascadeFirst(dim1Constraint)
                .CascadeFirst(dim2Constraint)
                .CascadeFirstOut(out val1)
                .CascadeFirstOut(out val2);

            return ok
                       ? new Pair(dim1Constraint, val1, dim2Constraint, val2)
                       : null;
        }

        public Pair FirstOrDefault(int dim1Constraint, int val1Constraint, int dim2Constraint)
        {
            int val2;
            bool ok = mPairs
                .CascadeFirst(dim1Constraint)
                .CascadeFirst(dim2Constraint)
                .CascadeFirst(val1Constraint)
                .CascadeFirstOut(out val2);

            return ok
                       ? new Pair(dim1Constraint, val1Constraint, dim2Constraint, val2)
                       : null;
        }

        public void Remove(Pair pair)
        {
            Remove(pair.Dim1, pair.Val1, pair.Dim2, pair.Val2);
        }
        public void Remove(int dim1, int val1, int dim2, int val2)
        {
            mPairs.CascadeRemove(dim1).CascadeRemove(dim2).CascadeRemove(val1).CascadeRemove(val2);
            mPairs.CascadeRemove(dim2).CascadeRemove(dim1).CascadeRemove(val2).CascadeRemove(val1);
        }
    }
}