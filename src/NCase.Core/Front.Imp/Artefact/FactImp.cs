using System;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Artefact
{
    public class FactImp : ArtefactImpBase<IFact, FactImp>, IFact
    {
        #region inner types

        public class Factory
        {
            private readonly IOperationDirector mOperationDirector;

            public Factory([NotNull] IOperationDirector operationDirector)
            {
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                mOperationDirector = operationDirector;
            }

            public virtual IFact Create(INode fact)
            {
                return new FactImp(fact, mOperationDirector);
            }
        }

        #endregion

        private readonly INode mFactNode;

        public FactImp([NotNull] INode factNode, [NotNull] IOperationDirector operationDirector)
            : base(operationDirector)
        {
            mFactNode = factNode;
        }

        protected override FactImp ThisDefImpl
        {
            get { return this; }
        }

        public INode FactNode
        {
            get { return mFactNode; }
        }
    }
}