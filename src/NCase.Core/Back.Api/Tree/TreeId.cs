using NCase.Back.Api.SetDef;

namespace NCase.Back.Api.Tree
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

        public override string DefTypeName
        {
            get { return "Tree"; }
        }
    }
}