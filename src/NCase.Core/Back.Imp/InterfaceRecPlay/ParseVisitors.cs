using Castle.DynamicProxy;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;
using NDsl.Api.RecPlay;

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
            ICodeLocation codeLocation = token.InvocationRecord.CodeLocation;
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