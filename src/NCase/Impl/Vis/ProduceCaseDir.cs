using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Lazy;

namespace NCase.Impl.Vis
{
    public class ProduceCaseDir : LazyDirector<INode, IProduceCaseDir>, IProduceCaseDir
    {
        private readonly Stack<INode> mCurrentCase = new Stack<INode>();

        public ProduceCaseDir(IEnumerable<ILazyVisitorClass<INode, IProduceCaseDir>> visitors)
            : base(visitors)
        {
        }

        public Stack<INode> CurrentCase
        {
            get { return mCurrentCase; }
        }
    }
}