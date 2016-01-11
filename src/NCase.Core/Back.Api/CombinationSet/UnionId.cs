using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
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
}