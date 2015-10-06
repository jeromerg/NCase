namespace NCase.Back.Api.Parse
{
    public class GenerateOptions
    {
        private readonly bool mIsRecursive;

        public GenerateOptions(bool isRecursive)
        {
            mIsRecursive = isRecursive;
        }

        public bool IsRecursive
        {
            get { return mIsRecursive; }
        }
    }
}