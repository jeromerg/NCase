using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
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