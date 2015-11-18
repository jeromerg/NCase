using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Seq
{
    public class SequenceId : SetDefId
    {
        public SequenceId([NotNull] string name)
            : base(name)
        {
        }

        [NotNull] public override string TypeName
        {
            get { return "Sequence"; }
        }
    }
}