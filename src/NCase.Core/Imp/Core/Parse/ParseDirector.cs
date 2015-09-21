using System.Collections.Generic;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Api.Action;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core.Parse
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

        public void AddReference(object reference, INode referencedNode)
        {
            mReferences.Add(reference, referencedNode);
        }

        public TNod GetReference<TNod>(object reference, ICodeLocation location) where TNod : INode
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
            mAddChildDirector.Visit(mCurrentScope, childNode);
        }
    }
}