using Castle.DynamicProxy;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NCase.Back.Imp.InterfaceRecPlay
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

            PropertyCallKey setterCallKey = invocation.TryGetPropertyCallKeyFromSetter();
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