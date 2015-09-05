using Castle.DynamicProxy;
using NCase.Api.Dev;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;
using NVisitor.Api.Batch;

namespace NCase.Imp.InterfaceRecPlay
{
    public class ParseVisitors
        : IVisitor<IToken, IParseDirector, InvocationToken<InterfaceRecPlayInterceptor>>
    {
        public void Visit(IParseDirector dir, InvocationToken<InterfaceRecPlayInterceptor> token)
        {
            ICodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            if (dir.CurrentSetNode == null)
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

            dir.CurrentSetNode.PlaceNextNode(newNode);
        }
    }
}