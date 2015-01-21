using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using NCase.Api;
using NCase.Api.Dev;
using NCase.Util.Castle;

namespace NCase.Core.InterfaceContrib
{
    public class ProxyInterceptor : IInterceptor, IReplayInterceptor
    {
        private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues = new Dictionary<PropertyCallKey, object>(); 
        private readonly AstNode mAstNode;

        public ProxyInterceptor(AstNode astNode)
        {
            mAstNode = astNode;
        }

        public void Intercept(IInvocation invocation)
        {
            if (mAstNode.IsReplaying)
                InterceptInReplayMode(invocation);
            else
                InterceptInRecordingMode(invocation);
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.SetterToPropertyCallKey(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in recording mode. Only property setter allowed", invocation.Method);

            mAstNode.Roots.Last().Children.Add(new InterfacePropertyNode(invocation, propertyCallKey, this));
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.GetterToPropertyCallKey(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in replay mode. Only property getter allowed", invocation.Method);

            object value;
            if (!mReplayPropertyValues.TryGetValue(propertyCallKey, out value))
                throw new CaseValueNotFoundException("Call to {0} cannot be replayed as it has not been recorded", invocation.Method);

            invocation.ReturnValue = value;
        }

        public void Replay(PropertyCallKey propertyInfo, object value)
        {
            mReplayPropertyValues[propertyInfo] = value;
        }
    }
}
