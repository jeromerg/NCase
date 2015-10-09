using NCase.Back.Api.SetDef;
using NCase.Back.Api.Tree;

namespace NCase.Back.Api.Seq
{
    public class SeqId : SetDefId
    {
        public SeqId()
        {
        }

        public SeqId(string name)
            : base(name)
        {
        }

        public override string DefTypeName
        {
            get { return "Seq"; }
        }
    }
}