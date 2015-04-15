using System;
using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Util;
using NVisitor.Api.Lazy;
using NVisitor.Common.Quality;

namespace NCase.Imp.Vis
{
    public class CaseGeneratorDirector : LazyDirector<INode, ICaseGeneratorDirector>, ICaseGeneratorDirector
    {
        private readonly Stack<INode> mCurrentCase = new Stack<INode>();

        public CaseGeneratorDirector(IEnumerable<ILazyVisitorClass<INode, ICaseGeneratorDirector>> visitors)
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