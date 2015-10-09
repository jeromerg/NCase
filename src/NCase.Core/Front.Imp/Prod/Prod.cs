using JetBrains.Annotations;
using NCase.Back.Api.Prod;
using NCase.Front.Api.Prod;
using NCase.Front.Imp.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Util;

namespace NCase.Front.Imp.Prod
{
    public class Prod : SetDef<IProdModel, ProdId>, IProd, IProdModel
    {
        public Prod([NotNull] string defName,
                    [NotNull] IBook book,
                    [NotNull] IServices<IProdModel> services,
                    [NotNull] ICodeLocationUtil codeLocationUtil)
            : base(new ProdId(defName), book, services, codeLocationUtil)
        {
        }

        public override IProdModel Model
        {
            get { return this; }
        }
    }
}