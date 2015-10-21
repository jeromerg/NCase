using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Seq
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