using System;
using Castle.DynamicProxy;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NCase.Imp.Vis.TokenStream
{
    public class ParseVisitors
        : IVisitor<IToken, IParseDirector, BeginToken<Api.TreeCaseSet>>
        , IVisitor<IToken, IParseDirector, EndToken<Api.TreeCaseSet>>
        , IVisitor<IToken, IParseDirector, InvocationToken<InterfaceRecPlayInterceptor>>
        , IVisitor<IToken, IParseDirector, RefToken<Api.TreeCaseSet>>
    {
        [NotNull] private readonly ITreeCaseSetInsertChildDirector mInsertChildDirector;

        public ParseVisitors([NotNull] ITreeCaseSetInsertChildDirector insertChildDirector)
        {
            if (insertChildDirector == null) throw new ArgumentNullException("insertChildDirector");
            mInsertChildDirector = insertChildDirector;
        }


        public void Visit(IParseDirector dir, BeginToken<Api.TreeCaseSet> token)
        {
            var newCaseSetNode = new TreeCaseSetNode(token.Owner, token.CodeLocation, mInsertChildDirector);
            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentCaseSetNode = newCaseSetNode;
        }

        public void Visit(IParseDirector dir, EndToken<Api.TreeCaseSet> token)
        {
            dir.CurrentCaseSetNode = null;
        }

        public void Visit(IParseDirector dir, InvocationToken<InterfaceRecPlayInterceptor> token)
        {
            ICodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            if (dir.CurrentCaseSetNode == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            PropertyCallKey setterCallKey = InvocationUtil.TryGetPropertyCallKeyFromSetter(invocation);
            if (setterCallKey == null)
            {
                throw new InvalidSyntaxException("While you build the cases, you can only call property setters on interface contributors: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            object argumentValue = invocation.GetArgumentValue(invocation.Arguments.Length - 1);

            var newNode = new InterfaceRecPlayNode(
                token.Owner,
                token.Owner.ContributorName,
                setterCallKey,
                argumentValue,
                codeLocation);

            dir.CurrentCaseSetNode.PlaceNextChild(newNode);
        }

        public void Visit(IParseDirector dir, RefToken<Api.TreeCaseSet> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            if (dir.CurrentCaseSetNode == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            ICaseSetNode referredCaseSetNode = dir.AllCaseSets[token.Owner];
            var newNode = new RefNode<ICaseSetNode>(referredCaseSetNode, codeLocation);

            dir.CurrentCaseSetNode.PlaceNextChild(newNode);
        }
    }
}