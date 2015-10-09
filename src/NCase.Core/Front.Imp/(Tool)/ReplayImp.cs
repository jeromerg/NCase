using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Replay;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp.Op
{
    public class ReplayCases : IReplayCases
    {
        [NotNull] private readonly IReplayDirector mReplayDirector;
        [NotNull] private readonly CaseEnumerablefactory mCaseEnumerableFactory;

        public ReplayCases([NotNull] IReplayDirector replayDirector, [NotNull] CaseEnumerablefactory caseEnumerableFactory)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mReplayDirector = replayDirector;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public ICaseEnumerable Perform(ICaseEnumerableModel caseEnumerableModel)
        {            
            IEnumerable<List<INode>> cases = Replay(caseEnumerableModel.Cases);
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