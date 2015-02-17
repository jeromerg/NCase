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
        [NotNull] private readonly IRecPlayContributorFactory mRecPlayContributorFactory;

        public CaseBuilder(
            [NotNull] IRecPlayContributorFactory recPlayContributorFactory,
            [NotNull] IRePlayDirector rePlayDirector,  // stateless director, no need for factory
            [NotNull] Func<IIterateCaseDirector> iterateCaseDirectorFactory
            )
        {
            if (recPlayContributorFactory == null) throw new ArgumentNullException("recPlayContributorFactory");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (iterateCaseDirectorFactory == null) throw new ArgumentNullException("iterateCaseDirectorFactory");

            mIterateCaseDirectorFactory = iterateCaseDirectorFactory;
            mRePlayDirector = rePlayDirector;
            mRecPlayContributorFactory = recPlayContributorFactory;
        }

        public T GetContributor<T>([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return mRecPlayContributorFactory.CreateInterface<T>(mAstRoot, name);
        }

        public CaseSet CreateSet([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            
            return new CaseSet(mAstRoot, name);
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