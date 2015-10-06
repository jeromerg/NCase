using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Artefact
{
    public class CaseImp : ArtefactImpBase<ICase, CaseImp>, ICase
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

            public virtual ICase Create(IEnumerable<INode> facts)
            {
                return new CaseImp(facts, mOperationDirector);
            }
        }

        #endregion

        private readonly IEnumerable<INode> mFactNodes;

        public CaseImp(IEnumerable<INode> factNodes, IOperationDirector operationDirector)
            : base(operationDirector)
        {
            mFactNodes = factNodes;
        }

        protected override CaseImp ThisDefImpl
        {
            get { return this; }
        }

        public IEnumerable<INode> FactNodes
        {
            get { return mFactNodes; }
        }
    }
}