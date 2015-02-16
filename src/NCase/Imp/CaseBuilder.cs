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
        [NotNull] private readonly Func<IIterateCaseDirector> mIterateCaseDirectorFactory;
        [NotNull] private readonly IRePlayDirector mRePlayDirector;
        private readonly IRecPlayContributorFactory mRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] Func<IIterateCaseDirector> iterateCaseDirectorFactory, 
            [NotNull] IRePlayDirector rePlayDirector,  // stateless director
            [NotNull] IRecPlayContributorFactory recPlayContributorFactory
            )
        {
            if (iterateCaseDirectorFactory == null) throw new ArgumentNullException("iterateCaseDirectorFactory");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (recPlayContributorFactory == null) throw new ArgumentNullException("recPlayContributorFactory");

            mIterateCaseDirectorFactory = iterateCaseDirectorFactory;
            mRePlayDirector = rePlayDirector;
            mRecPlayContributorFactory = recPlayContributorFactory;
        }

        public T GetContributor<T>([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mRecPlayContributorFactory.CreateInterface<T>(mAstRoot, name);
        }

        public CaseSet NewSet([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            
            var caseSetNode = new CaseSetNode(name);
            mAstRoot.AddChild(caseSetNode);
            return new CaseSet(caseSetNode);
        }

        public IEnumerable<Pause> PlayAllCases()
        {
            mAstRoot.State = AstState.Processing;
            

            mAstRoot.State = AstState.Reading;

            IIterateCaseDirector iterateCaseDirector = mIterateCaseDirectorFactory();
            foreach (var pause in iterateCaseDirector.Visit(mAstRoot))
            {
                // TODO: IMPROVE REPLAY BY INTRODUCING VALUE-CLEANING AFTER EACH REPLAY!
                ReplayCase(iterateCaseDirector.CurrentCase);
                yield return Pause.Now;
            } 

        }

        private void ReplayCase(IEnumerable<INode> nodes)
        {
            foreach (var node in nodes)
                mRePlayDirector.Visit(node);
        }
    }
}