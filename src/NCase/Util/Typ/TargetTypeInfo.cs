namespace NTestCase.Util.Typ
{
    public class TargetTypeInfo
    {
        private readonly System.Type mType;
        private readonly TargetTypeStatus mStatus;

        public TargetTypeInfo(System.Type type, TargetTypeStatus status)
        {
            mType = type;
            mStatus = status;
        }

        public System.Type Type
        {
            get { return mType; }
        }

        public TargetTypeStatus Status
        {
            get { return mStatus; }
        }
    }
}