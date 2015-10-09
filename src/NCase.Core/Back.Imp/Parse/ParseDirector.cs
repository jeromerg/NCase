using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Parse
{
    public class ParseDirector : ActionDirector<IToken, IParseDirector>, IParseDirector
    {
        private readonly IAddChildDirector mAddChildDirector;
        private readonly Dictionary<object, INode> mReferences = new Dictionary<object, INode>();
        [CanBeNull] private INode mCurrentScope;

        public ParseDirector(IActionVisitMapper<IToken, IParseDirector> visitMapper, IAddChildDirector addChildDirector)
            : base(visitMapper)
        {
            mAddChildDirector = addChildDirector;
        }

        public void AddId(object reference, INode referencedNode)
        {
            mReferences.Add(reference, referencedNode);
        }

        public TNod GetReferencedNode<TNod>(object reference, CodeLocation location) where TNod : INode
        {
            INode referencedNode;
            if (!mReferences.TryGetValue(reference, out referencedNode))
            {
                throw new InvalidSyntaxException(location, "No reference found for: {0}", reference);
            }

            if (!(referencedNode is TNod))
            {
                throw new InvalidSyntaxException(location,
                                                 "Referenced Node {0} expected to be assignable to {1}",
                                                 reference.GetType().FullName,
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