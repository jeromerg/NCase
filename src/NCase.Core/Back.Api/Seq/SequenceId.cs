using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Seq
{
    public class SequenceId : SetDefId
    {
        public SequenceId(string name)
            : base(name)
        {
        }

        public override string TypeName
        {
            get { return "Sequence"; }
        }
    }
}