using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Front.Imp
{
    public class ReplayCases : IReplayCases
    {
        [NotNull] private readonly IReplayDirector mReplayDirector;
        [NotNull] private readonly ICaseEnumerableFactory mCaseEnumerableFactory;

        public ReplayCases([NotNull] IReplayDirector replayDirector, [NotNull] ICaseEnumerableFactory caseEnumerableFactory)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            if (caseEnumerableFactory == null) throw new ArgumentNullException("caseEnumerableFactory");
            mReplayDirector = replayDirector;
            mCaseEnumerableFactory = caseEnumerableFactory;
        }

        public CaseEnumerable Perform(ICaseEnumerableModel caseEnumerableModel)
        {
            IEnumerable<List<INode>> cases = Replay(caseEnumerableModel.Cases);
            return mCaseEnumerableFactory.Create(cases);
        }

        private IEnumerable<List<INode>> Replay(IEnumerable<List<INode>> cases)
        {
            foreach (List<INode> cas in cases)
            {
                foreach (INode node in cas)
                    mReplayDirector.Visit(node, true);

                yield return cas;

                foreach (INode node in cas)
                    mReplayDirector.Visit(node, false);
            }
        }
    }
}