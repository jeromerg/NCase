using System;
using System.Linq;
using Castle.DynamicProxy;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NDsl.Imp.Core.Token;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NCase.Imp.Vis
{
    public class ParserVisitors
        : IVisitor<IToken, IParserDirector, BeginToken<TreeCaseSet>>
        , IVisitor<IToken, IParserDirector, EndToken<TreeCaseSet>>
        , IVisitor<IToken, IParserDirector, InvocationToken<InterfaceRecPlayInterceptor>>
        , IVisitor<IToken, IParserDirector, RefToken<TreeCaseSet>>
    {
        [NotNull] private readonly ITreeCaseSetInsertChildDirector mInsertChildDirector;

        public ParserVisitors([NotNull] ITreeCaseSetInsertChildDirector insertChildDirector)
        {
            if (insertChildDirector == null) throw new ArgumentNullException("insertChildDirector");
            mInsertChildDirector = insertChildDirector;
        }


        public void Visit(IParserDirector dir, BeginToken<TreeCaseSet> token)
        {
            var newCaseSetNode = new TreeCaseSetNode(token.Owner, token.CodeLocation, mInsertChildDirector);
            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentCaseSetNode = newCaseSetNode;
        }

        public void Visit(IParserDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.CurrentCaseSetNode = null;
        }

        public void Visit(IParserDirector dir, InvocationToken<InterfaceRecPlayInterceptor> token)
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

            dir.CurrentCaseSetNode.InsertChild(newNode);
        }

        public void Visit(IParserDirector dir, RefToken<TreeCaseSet> node)
        {
            //TODO JRG HERE CONTINUE dir.CurrentCaseSetNode 
        }
    }
}