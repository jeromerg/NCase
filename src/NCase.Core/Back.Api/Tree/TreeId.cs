using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Tree
{
    public class TreeId : SetDefId
    {
        public TreeId()
        {
        }

        public TreeId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Tree"; }
        }
    }
}