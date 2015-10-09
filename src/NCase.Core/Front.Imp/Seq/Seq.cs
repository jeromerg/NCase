using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NCase.Front.Api.Seq;
using NCase.Front.Imp.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Seq
{
    public class Seq : SetDef<ISeqModel, SeqId>, ISeq, ISeqModel
    {
        public Seq([NotNull] string defName,
                   [NotNull] IBook book,
                   [NotNull] IServices<ISeqModel> services,
                   [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new SeqId(defName), book, services, codeLocationUtil)
        {
        }

        public override ISeqModel Model
        {
            get { return this; }
        }
    }
}