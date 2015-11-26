using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public class CombinationsId : SetDefId
    {
        public CombinationsId()
        {
        }

        public CombinationsId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Combinations"; }
        }
    }
}