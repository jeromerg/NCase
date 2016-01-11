using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class PairwiseProdNode : ProdNode, IPairwiseProdNode
    {
        [NotNull] private readonly PairwiseProdId mId;

        public PairwiseProdNode([NotNull] PairwiseProdId id, [NotNull] CodeLocation codeLocation)
            : base(id, codeLocation)
        {
            mId = id;
        }

        public new PairwiseProdId Id
        {
            get { return mId; }
        }
    }
}