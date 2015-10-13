using System.Collections.Generic;

namespace NCase.Back.Imp.Pairwise
{
    public class PairwiseGenerator
    {
        private class ValueDictionary
        {
            private readonly Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>> mPairs = new Dictionary<int, Dictionary<int, Dictionary<int, HashSet<int>>>>();

            public void Add(int dim1, int value1, int dim2, int value2)
            {
                mPairs.GetOrCreate(dim1).GetOrCreate(value1).GetOrCreate(dim2).Add(value2);
                mPairs.GetOrCreate(dim2).GetOrCreate(value2).GetOrCreate(dim1).Add(value1);
            }

            public void Remove(int dim1, int value1, int dim2, int value2)
            {
                mPairs.GetOrCreate(dim1).GetOrCreate(value1).GetOrCreate(dim2).Add(value2);
                mPairs.GetOrCreate(dim2).GetOrCreate(value2).GetOrCreate(dim1).Add(value1);
            }
        }

    }
}
