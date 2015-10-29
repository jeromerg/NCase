namespace NUtil.Math.Combinatorics.Pairwise
{
    public class DimValue
    {
        private readonly int mDim;
        private readonly int mVal;

        public DimValue(int dim, int val)
        {
            mDim = dim;
            mVal = val;
        }

        public int Dim
        {
            get { return mDim; }
        }

        public int Val
        {
            get { return mVal; }
        }
    }
}