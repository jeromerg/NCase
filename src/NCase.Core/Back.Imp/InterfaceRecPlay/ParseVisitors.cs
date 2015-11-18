using Castle.DynamicProxy;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class ParseVisitors
        : IParseVisitor<InvocationToken<IInterfaceRecPlayInterceptor>>
    {
        [NotNull] private readonly IInterfaceReIInterfaceRecPlayNodeFactory mNodeFactory;

        public ParseVisitors([NotNull] IInterfaceReIInterfaceRecPlayNodeFactory nodeFactory)
        {
            mNodeFactory = nodeFactory;
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] InvocationToken<IInterfaceRecPlayInterceptor> token)
        {
            CodeLocation codeLocation = token.InvocationRecord.CodeLocation;
            IInvocation invocation = token.InvocationRecord.Invocation;

            PropertyCallKey setterCallKey = invocation.TryGetPropertyCallKeyFromSetter();
            if (setterCallKey == null)
            {
                throw new InvalidSyntaxException(token.CodeLocation,
                                                 "While definining test cases, you can only call property setters on interface contributors");
            }

            // ReSharper disable once PossibleNullReferenceException
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