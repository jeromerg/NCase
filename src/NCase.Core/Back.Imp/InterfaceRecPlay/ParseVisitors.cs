using Castle.DynamicProxy;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.RecPlay
{
    public class ParseVisitors
        : IParseVisitor<InvocationToken<IInterfaceRecPlayInterceptor>>
    {
        private readonly IInterfaceReIInterfaceRecPlayNodeFactory mNodeFactory;

        public ParseVisitors(IInterfaceReIInterfaceRecPlayNodeFactory nodeFactory)
        {
            mNodeFactory = nodeFactory;
        }

        public void Visit(IParseDirector dir, InvocationToken<IInterfaceRecPlayInterceptor> token)
        {
            CodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            PropertyCallKey setterCallKey = InvocationUtil.TryGetPropertyCallKeyFromSetter(invocation);
            if (setterCallKey == null)
            {
                throw new InvalidSyntaxException(token.CodeLocation,
                                                 "While definining test cases, you can only call property setters on interface contributors");
            }

            object argumentValue = invocation.GetArgumentValue(invocation.Arguments.Length - 1);

            IInterfaceRecPlayNode newNode = mNodeFactory.Create(token.Owner,
                                                                token.Owner.ContributorName,
                                                                setterCallKey,
                                                                argumentValue,
                                                                codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}