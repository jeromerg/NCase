using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Tree
{
    public class TreeId : SetDefId
    {
        public TreeId()
        {
        }

        public TreeId(string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "Tree"; }
        }
    }
}