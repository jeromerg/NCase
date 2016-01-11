using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class CombinationSetId : SetDefId
    {
        public CombinationSetId()
        {
        }

        public CombinationSetId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Combination"; }
        }
    }
}