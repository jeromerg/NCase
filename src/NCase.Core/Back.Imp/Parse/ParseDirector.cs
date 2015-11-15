using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.All.Common;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Imp.Parse
{
    public class ParseDirector : ActionDirector<IToken, IParseDirector>, IParseDirector
    {
        private readonly IAddChildDirector mAddChildDirector;
        private readonly Dictionary<IId, INode> mDefinitions = new Dictionary<IId, INode>();
        [CanBeNull] private INode mCurrentScope;

        public ParseDirector(IActionVisitMapper<IToken, IParseDirector> visitMapper, IAddChildDirector addChildDirector)
            : base(visitMapper)
        {
            mAddChildDirector = addChildDirector;
        }

        public void AddId(IId id, INode referencedNode)
        {
            mDefinitions.Add(id, referencedNode);
        }

        public TNod GetNodeForId<TNod>(IId id, CodeLocation location) where TNod : INode
        {
            INode referencedNode;
            if (!mDefinitions.TryGetValue(id, out referencedNode))
            {
                throw new InvalidSyntaxException(location, "No entry found for: {0}", id);
            }

            if (!(referencedNode is TNod))
            {
                throw new InvalidSyntaxException(location,
                                                 "Node {0} expected to be assignable to {1}",
                                                 id.GetType().FullName,
                                                 typeof (TNod).FullName);
            }
            return (TNod) referencedNode;
        }

        public void PushScope(INode rootNode)
        {
            mCurrentScope = rootNode;
        }

        public void PopScope()
        {
            mCurrentScope = null;
        }

        public void AddChildToScope(INode childNode)
        {
            if (mCurrentScope == null)
                throw new InvalidSyntaxException(childNode.CodeLocation, "Trying to add child outside of any scope");

            mAddChildDirector.Visit(mCurrentScope, childNode);
        }
    }
}