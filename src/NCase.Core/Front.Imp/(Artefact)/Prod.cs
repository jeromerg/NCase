using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Prod : SetDef<ProdId, IProdApi>, IProd, IProdApi
    {
        public Prod([NotNull] string defName, [NotNull] IBook book, [NotNull] ITools tools)
            : base(new ProdId(defName), book, tools)
        {
        }

        public override IProdApi Api
        {
            get { return this; }
        }
    }
}