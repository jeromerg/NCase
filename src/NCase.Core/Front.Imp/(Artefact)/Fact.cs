using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Fact : Artefact<IFactApi>, IFact, IFactApi
    {
        private readonly INode mFactNode;

        public Fact([NotNull] INode factNode, [NotNull] IToolBox<IFactApi> toolBox)
            : base(toolBox)
        {
            mFactNode = factNode;
        }

        protected override IFactApi GetApi()
        {
            return this;
        }

        public INode FactNode
        {
            get { return mFactNode; }
        }

    }
}