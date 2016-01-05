using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public class ProdId : SetDefId
    {
        public ProdId()
        {
        }

        public ProdId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Product"; }
        }
    }    
    
    public class UnionId : SetDefId
    {
        public UnionId()
        {
        }

        public UnionId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Union"; }
        }
    }    
    
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