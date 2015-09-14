using Castle.DynamicProxy;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NDsl.Imp.RecPlay;
using NDsl.Util.Castle;

namespace NCase.Imp.InterfaceRecPlay
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
                token.Owner,
                token.Owner.ContributorName,
                setterCallKey,
                argumentValue,
                codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}