using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Prod
{
    public class ProdId : SetDefId
    {
        public ProdId()
        {
        }

        public ProdId(string name)
            : base(name)
        {
        }

        public override string DefTypeName
        {
            get { return "AllCombinations"; }
        }
    }
}