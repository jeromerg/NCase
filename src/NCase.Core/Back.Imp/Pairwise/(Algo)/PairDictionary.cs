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
            foreach(int dim1 in Enumerable.Range(0, dimSizes.Count))
            {
                int dimSize1 = dimSizes[dim1];

                foreach (int dim2 in Enumerable.Range(0, dimSizes.Count))
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

        public bool Any()
        {
            return mPairs.Any();
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

        public bool TryRemoveFirst(int dim1, out int val1, int dim2, out int val2)
        {
            bool ok = mPairs
                .CascadeFirst(dim1)
                .CascadeFirst(dim2)
                .CascadeFirstOut(out val1)
                .CascadeFirstOut(out val2);

            if(ok)
                Remove(dim1, val1, dim2, val2);            
            
            return ok;
        }

        public bool TryRemoveFirst(int dim1, int val1, int dim2, out int val2)
        {
            bool ok = mPairs
                .CascadeFirst(dim1)
                .CascadeFirst(dim2)
                .CascadeFirst(val1)
                .CascadeFirstOut(out val2);

            if(ok)
                Remove(dim1, val1, dim2, val2);            
            
            return ok;
        }

        public void Remove(int dim1, int val1, int dim2, int val2)
        {
            mPairs.CascadeRemove(dim1).CascadeRemove(dim2).CascadeRemove(val1).CascadeRemove(val2);
            mPairs.CascadeRemove(dim2).CascadeRemove(dim1).CascadeRemove(val2).CascadeRemove(val1);
        }
    }
}