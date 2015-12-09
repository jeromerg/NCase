using System;
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
        [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;
        [NotNull] private readonly IAddChildDirector mAddChildDirector;
        [NotNull] private readonly Dictionary<IId, INode> mDefinitions = new Dictionary<IId, INode>();
        [CanBeNull] private INode mCurrentScope;

        public ParseDirector([NotNull]ICodeLocationPrinter codeLocationPrinter,
            [NotNull] IActionVisitMapper<IToken, IParseDirector> visitMapper,
                             [NotNull] IAddChildDirector addChildDirector)
            : base(visitMapper)
        {
            mCodeLocationPrinter = codeLocationPrinter;
            mAddChildDirector = addChildDirector;
        }

        public void AddId([NotNull] IId id, [NotNull] INode referencedNode)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (referencedNode == null) throw new ArgumentNullException("referencedNode");

            mDefinitions.Add(id, referencedNode);
        }

        [NotNull]
        public TNod GetNodeForId<TNod>([NotNull] IId id, [NotNull] CodeLocation location)
            where TNod : INode
        {
            if (id == null) throw new ArgumentNullException("id");
            if (location == null) throw new ArgumentNullException("location");

            INode referencedNode;
            if (!mDefinitions.TryGetValue(id, out referencedNode))
            {
                throw new InvalidSyntaxException(mCodeLocationPrinter, location, "No entry found for: {0}", id);
            }

            if (!(referencedNode is TNod))
            {
                throw new InvalidSyntaxException(mCodeLocationPrinter, location,
                                                 "Node {0} expected to be assignable to {1}",
                                                 id.GetType().FullName,
                                                 typeof (TNod).FullName);
            }
            return (TNod) referencedNode;
        }

        public void PushScope([NotNull] INode rootNode)
        {
            if (rootNode == null) throw new ArgumentNullException("rootNode");

            mCurrentScope = rootNode;
        }

        public void PopScope()
        {
            mCurrentScope = null;
        }

        public void AddChildToScope([NotNull] INode childNode)
        {
            if (childNode == null) throw new ArgumentNullException("childNode");

            if (mCurrentScope == null)
                throw new InvalidSyntaxException(mCodeLocationPrinter, childNode.CodeLocation, "Trying to add child outside of any scope");

            mAddChildDirector.Visit(mCurrentScope, childNode);
        }
    }
}