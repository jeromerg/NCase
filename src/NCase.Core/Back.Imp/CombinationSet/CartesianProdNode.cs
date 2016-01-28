using JetBrains.Annotations;
using NCaseFramework.Back.Api.CombinationSet;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.CombinationSet
{
    public class CartesianProdNode : ProdNode, ICartesianProdNode
    {
        [NotNull] private readonly CartesianProdId mId;

        public CartesianProdNode([NotNull] CartesianProdId id, [NotNull] CodeLocation codeLocation)
            : base(id, codeLocation)
        {
            mId = id;
        }

        public new CartesianProdId Id
        {
            get { return mId; }
        }
    }
}