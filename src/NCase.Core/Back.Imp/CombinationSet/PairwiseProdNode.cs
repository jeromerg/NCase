using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
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