namespace NUtil.Math.Combinatorics.Pairwise
{
    public class Pair
    {
        private readonly int mDim1;
        private readonly int mVal1;
        private readonly int mDim2;
        private readonly int mVal2;

        public Pair(int dim1, int val1, int dim2, int val2)
        {
            mDim1 = dim1;
            mVal1 = val1;
            mDim2 = dim2;
            mVal2 = val2;
        }

        public int Dim1
        {
            get { return mDim1; }
        }

        public int Val1
        {
            get { return mVal1; }
        }

        public int Dim2
        {
            get { return mDim2; }
        }

        public int Val2
        {
            get { return mVal2; }
        }

        public override string ToString()
        {
            return string.Format("dim1 ({0}, {1}) dim2: ({2}, {3})", mDim1, mVal1, mDim2, mVal2);
        }
    }
}