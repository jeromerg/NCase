using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Prod
{
    public class AllCombinationsId : SetDefId
    {
        public AllCombinationsId(string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "AllCombinations"; }
        }
    }
}