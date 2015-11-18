using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Prod
{
    public class AllCombinationsId : SetDefId
    {
        public AllCombinationsId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "AllCombinations"; }
        }
    }
}