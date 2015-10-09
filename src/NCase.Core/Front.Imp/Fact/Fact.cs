using JetBrains.Annotations;
using NCase.Front.Api.Fact;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Fact
{
    public class Fact : Artefact<IFactModel>, IFact, IFactModel
    {
        private readonly INode mFactNode;

        public Fact([NotNull] INode factNode, [NotNull] IServices<IFactModel> services)
            : base(services)
        {
            mFactNode = factNode;
        }

        public override IFactModel Model
        {
            get { return this; }
        }

        #region IFactModel

        public INode FactNode
        {
            get { return mFactNode; }
        }

        #endregion
    }
}