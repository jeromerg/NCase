﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NCase.Back.Imp.Pairwise
{
    public class Tuple
    {
        private readonly int[] mResult;

        public Tuple(int amountOfDims)
        {
            mResult = Enumerable.Repeat(-1, amountOfDims).ToArray();
        }

        public void Add(int dim, int val)
        {
            int existingVal = mResult[dim];
            if (existingVal != -1 && existingVal != val)
                throw new ArgumentException(string.Format("dim {0} is already set to another value", dim));

            mResult[dim] = val;
        }

        public IEnumerable<int> FreeDims
        {
            get
            {
                for (int i = 0; i < mResult.Length; i++)
                {
                    if (mResult[i] == -1)
                        yield return i;
                }
            }
        }

        public IEnumerable<DimValue> FrozenDimValues
        {
            get
            {
                for (int i = 0; i < mResult.Length; i++)
                {
                    if (mResult[i] != -1)
                        yield return new DimValue(i, mResult[i]);
                }
            }
        }

        public int[] Result
        {
            get { return mResult; }
        }
    }
}