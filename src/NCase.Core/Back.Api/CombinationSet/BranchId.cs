using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class BranchId : SetDefId
    {
        public BranchId()
        {
        }

        public BranchId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Branch"; }
        }
    }
}