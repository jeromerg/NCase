using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Pairwise : SetDef<PairwiseId, IPairwiseApi>, IPairwise, IPairwiseApi
    {
        public Pairwise([NotNull] string defName, [NotNull] IBook book, [NotNull] ITools tools)
            : base(new PairwiseId(defName), book, tools)
        {
        }

        public override IPairwiseApi Api
        {
            get { return this; }
        }
    }
}