using Castle.DynamicProxy;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Imp.RecPlay;

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