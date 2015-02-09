using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NDsl.Impl.Core;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Impl
{
    public class CaseBuilder : ICaseBuilder
    {
        [NotNull] private readonly AstRoot mAstRoot = new AstRoot();
        [NotNull] private readonly Func<IProduceCaseDir> mProduceCaseDirFactory;
        [NotNull] private readonly IRePlayDir mRePlayDir;
        private readonly IRecPlayContributorFactory mRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] Func<IProduceCaseDir> produceCaseDirFactory, 
            [NotNull] IRePlayDir rePlayDir,  // stateless director
            [NotNull] IRecPlayContributorFactory recPlayContributorFactory
            )
        {
            if (produceCaseDirFactory == null) throw new ArgumentNullException("produceCaseDirFactory");
            if (rePlayDir == null) throw new ArgumentNullException("rePlayDir");
            if (recPlayContributorFactory == null) throw new ArgumentNullException("recPlayContributorFactory");

            mProduceCaseDirFactory = produceCaseDirFactory;
            mRePlayDir = rePlayDir;
            mRecPlayContributorFactory = recPlayContributorFactory;
        }

        public T GetContributor<T>(string name)
        {
            return mRecPlayContributorFactory.CreateInterface<T>(mAstRoot, name);
        }

        public CaseSet CaseSet(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            var caseSetNode = new CaseSetNode(name);
            mAstRoot.AddChild(caseSetNode);
            return new CaseSet(caseSetNode);
        }

        public IEnumerable<Pause> GetAllCases()
        {
            mAstRoot.State = AstState.Reading;
            IProduceCaseDir produceCaseDir = mProduceCaseDirFactory();
            foreach (var pause in produceCaseDir.Visit(mAstRoot))
            {
                // TODO: IMPROVE REPLAY BY INTRODUCING VALUE-CLEANING AFTER EACH REPLAY!
                ReplayCase(produceCaseDir);
                yield return Pause.Now;
            } 
        }

        private void ReplayCase(IProduceCaseDir produceCaseDir)
        {
            foreach (var node in produceCaseDir.CurrentCase)
                mRePlayDir.Visit(node);
        }
    }
}