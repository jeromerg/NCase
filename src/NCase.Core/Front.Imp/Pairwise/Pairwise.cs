using System;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api.Pairwise;
using NCase.Front.Imp.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Pairwise
{
    public class Pairwise : SetDef<IPairwiseModel, PairwiseId>, IPairwise, IPairwiseModel
    {
        public Pairwise([NotNull] string defName,
                        [NotNull] ITokenStream tokenStream,
                        [NotNull] IServices<IPairwiseModel> services,
                        [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new PairwiseId(defName), tokenStream, services, codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
        }

        public override IPairwiseModel Model
        {
            get { return this; }
        }
    }
}