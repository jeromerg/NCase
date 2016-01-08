using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public class CombinationId : SetDefId
    {
        public CombinationId()
        {
        }

        public CombinationId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Combination"; }
        }
    }
}