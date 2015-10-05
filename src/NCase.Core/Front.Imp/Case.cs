using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Replay;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Case : ArtefactImpBase<ICase, Case>, ICase
    {
        #region inner types

        public class Factory : ICaseFactory
        {
            private readonly IReplayDirector mReplayDirector;
            private readonly IOperationDirector mOperationDirector;

            public Factory([NotNull] IReplayDirector replayDirector, [NotNull] IOperationDirector operationDirector)
            {
                if (replayDirector == null) throw new ArgumentNullException("replayDirector");
                if (operationDirector == null) throw new ArgumentNullException("operationDirector");
                mReplayDirector = replayDirector;
                mOperationDirector = operationDirector;
            }

            public ICase Create(IEnumerable<INode> facts)
            {
                return new Case(facts, mReplayDirector, mOperationDirector);
            }
        }

        #endregion

        private readonly IReplayDirector mReplayDirector;
        private readonly IEnumerable<INode> mFactNodes;

        public Case(IEnumerable<INode> factNodes, IReplayDirector replayDirector, IOperationDirector operationDirector)
            : base(operationDirector)
        {
            mFactNodes = factNodes;
            mReplayDirector = replayDirector;
        }

        protected override Case ThisDefImpl
        {
            get { return this; }
        }

        public void Replay(bool isReplay)
        {
            foreach (INode factNode in mFactNodes)
                mReplayDirector.Visit(factNode, isReplay);
        }
    }
}