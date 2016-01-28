using JetBrains.Annotations;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class CombinationSetBeginToken : BeginToken<CombinationSetId> 
    {
        private readonly bool mOnlyPairwise;

        public CombinationSetBeginToken([NotNull] CombinationSetId owner, [NotNull] CodeLocation codeLocation, bool onlyPairwise)
            : base(owner, codeLocation)
        {
            mOnlyPairwise = onlyPairwise;
        }

        public bool OnlyPairwise
        {
            get { return mOnlyPairwise; }
        }
    }
}