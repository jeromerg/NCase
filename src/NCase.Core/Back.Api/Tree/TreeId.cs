using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Tree
{
    public class TreeId : DefId
    {
        private string mDefTypeName;

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