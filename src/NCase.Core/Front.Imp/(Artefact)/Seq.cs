using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Seq : SetDef<SeqId, ISeqApi>, ISeq, ISeqApi
    {
        public Seq([NotNull] string defName, [NotNull] IBook book, [NotNull] ITools tools)
            : base(new SeqId(defName), book, tools)
        {
        }

        public override ISeqApi Api
        {
            get { return this; }
        }
    }
}