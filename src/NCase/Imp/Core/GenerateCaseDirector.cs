using System;
using System.Collections.Generic;
using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NDsl.Util;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class GenerateCaseDirector : LazyDirector<INode, IGenerateCaseDirector>, IGenerateCaseDirector
    {
        private readonly Stack<INode> mCurrentCase = new Stack<INode>();

        public GenerateCaseDirector(IEnumerable<ILazyVisitorClass<INode, IGenerateCaseDirector>> visitors)
            : base(visitors)
        {
        }

        [NotNull]
        public IDisposable Push([NotNull] INode node)
        {
            mCurrentCase.Push(node);
            return new Disposable(() => mCurrentCase.Pop());
        }

        [NotNull]
        public IEnumerable<INode> CurrentCase
        {
            get { return mCurrentCase; }
        }
    }
}