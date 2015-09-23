using Castle.DynamicProxy;
using NCase.Back.Api.Core.Parse;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class ParseVisitors
        : IParseVisitor<InvocationToken<InterfaceRecPlayInterceptor>>
    {
        public void Visit(IParseDirector dir, InvocationToken<InterfaceRecPlayInterceptor> token)
        {
            ICodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            PropertyCallKey setterCallKey = InvocationUtil.TryGetPropertyCallKeyFromSetter(invocation);
            if (setterCallKey == null)
            {
                throw new InvalidSyntaxException(token.CodeLocation,
                                                 "While definining test cases, you can only call property setters on interface contributors");
            }

            object argumentValue = invocation.GetArgumentValue(invocation.Arguments.Length - 1);

            var newNode = new InterfaceRecPlayNode(
                token.OwnerId,
                token.OwnerId.ContributorName,
                setterCallKey,
                argumentValue,
                codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}