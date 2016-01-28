using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public abstract class ProdId : SetDefId
    {
        protected ProdId()
        {
        }

        protected ProdId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Product"; }
        }
    }
}