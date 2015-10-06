using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Tree
{
    public class TreeId : DefId
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