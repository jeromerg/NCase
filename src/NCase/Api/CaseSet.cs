namespace NCase.Api
{
    public class CaseSet
    {
        private readonly ICaseSetNode mNode;

        public CaseSet(ICaseSetNode node)
        {
            mNode = node;
        }

        internal ICaseSetNode Node
        {
            get { return mNode; }
        }
    }
}