using System;
using System.Collections.Generic;
using System.Linq;

namespace NCase.Back.Imp.Pairwise
{
    public class PairDictionary
    {
        private readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> mPairs
            = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();

        public PairDictionary()
        {
        }

        public PairDictionary(IList<int> dimSizes)
        {
            // fill in with all pairs between all dimensions
            for (int dim1 = 0; dim1 < dimSizes.Count; dim1++)
            {
                int dimSize1 = dimSizes[dim1];

                for (int dim2 = 0; dim2 < dimSizes.Count; dim2++)
                {
                    if (dim2 == dim1)
                        continue;

                    int dimSize2 = dimSizes[dim2];

                    foreach (int val1 in Enumerable.Range(0, dimSize1))
                        foreach (int val2 in Enumerable.Range(0, dimSize2))
                            Add(dim1, val1, dim2, val2);
                }
            }
        }

        public void Add(int dim1, int val1, int dim2, int val2)
        {
            mPairs.GetOrCreate(dim1).GetOrCreate(val1).GetOrCreate(dim2).Add(val2);
            mPairs.GetOrCreate(dim2).GetOrCreate(val2).GetOrCreate(dim1).Add(val1);
        }

        public bool TryRemoveFirst(int dim1, out int val1, out int dim2, out int val2)
        {
            Dictionary<int, Dictionary<int, HashSet<int>>> val1Dict;
            if (!mPairs.TryGetValue(dim1, out val1Dict))
            {
                val1 = dim2 = val2 = -1;
                return false;
            }

            val1Dict.CascadeFirst(out val1).CascadeFirst(out dim2).CascadeFirst(out val2);
            Remove(dim1, val1, dim2, val2);            
            return true;
        }

        public void RemoveFirst(out int dim1, out int val1, out int dim2, out int val2)
        {
            if (!mPairs.Any())
                throw new ArgumentException();

            mPairs.CascadeFirst(out dim1).CascadeFirst(out val1).CascadeFirst(out dim2).CascadeFirst(out val2);            
            Remove(dim1, val1, dim2, val2);
        }

        private void Remove(int dim1, int val1, int dim2, int val2)
        {
            mPairs.CascadeClean(dim1).CascadeClean(val1).CascadeClean(dim2).Remove(val2);
            mPairs.CascadeClean(dim2).CascadeClean(val2).CascadeClean(dim1).Remove(val1);
        }

        public bool Any()
        {
            return mPairs.Any();
        }
    }
}