using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Prod
{
    public class ProdId : DefId
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