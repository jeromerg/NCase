using NCase.Back.Api.SetDef;
using NCase.Back.Api.Tree;

namespace NCase.Back.Api.Prod
{
    public class ProdId : SetDefId
    {
        public ProdId()
        {
        }

        public ProdId(string name)
            : base(name)
        {
        }

        public override string DefTypeName
        {
            get { return "Prod"; }
        }
    }
}