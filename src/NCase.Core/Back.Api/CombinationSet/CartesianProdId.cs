using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class CartesianProdId : ProdId
    {
        public CartesianProdId()
        {
        }

        public CartesianProdId([NotNull] string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "Cartesian Product"; }
        }
    }
}