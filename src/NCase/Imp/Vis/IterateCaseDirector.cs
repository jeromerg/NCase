using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Impl.Vis
{
    public class IterateCaseDirector : LazyDirector<INode, IIterateCaseDirector>, IIterateCaseDirector
    {
        private readonly Stack<INode> mCurrentCase = new Stack<INode>();

        public IterateCaseDirector(IEnumerable<ILazyVisitorClass<INode, IIterateCaseDirector>> visitors)
            : base(visitors)
        {
        }

        public Stack<INode> CurrentCase
        {
            get { return mCurrentCase; }
        }
    }
}