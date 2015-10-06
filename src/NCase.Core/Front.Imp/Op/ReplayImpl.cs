using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Replay;
using NCase.Front.Imp.Artefact;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;
using NDsl.Front.Api;

namespace NCase.Front.Imp.Op
{
    public class ReplayImp : IOperationImp<ICaseEnumerable, Replay, CaseEnumerableImp, ICaseEnumerable>
    {
        [NotNull] private readonly IReplayDirector mReplayDirector;
        [NotNull] private readonly CaseEnumerableImp.Factory mCaseEnumerableFactory;

        public ReplayImp([NotNull] IReplayDirector replayDirector, [NotNull] CaseEnumerableImp.Factory caseEnumerableFactory)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mReplayDirector = replayDirector;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(IOperationDirector director, Replay replay, CaseEnumerableImp caseEnumerable)
        {
            IEnumerable<List<INode>> cases = Replay(caseEnumerable.Cases);
            return mCaseEnumerableFactory.Create(cases);
        }

        private IEnumerable<List<INode>> Replay(IEnumerable<List<INode>> cases)
        {
            foreach (List<INode> @case in cases)
            {
                foreach (INode node in @case)
                    mReplayDirector.Visit(node, true);

                yield return @case;

                foreach (INode node in @case)
                    mReplayDirector.Visit(node, false);
            }
        }
    }
}