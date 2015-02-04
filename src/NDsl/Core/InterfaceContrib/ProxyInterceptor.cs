using System.Collections.Generic;
using Castle.DynamicProxy;
using NDsl.Api;
using NDsl.Api.Dev;
using NDsl.Util.Castle;

namespace NDsl.Core.InterfaceContrib
{
    public class ProxyInterceptor : IInterceptor, IReplayInterceptor
    {
        private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues = new Dictionary<PropertyCallKey, object>(); 
        private readonly RootNode mRootNode;

        // TODO: inject StackTraceUtil
        public ProxyInterceptor(RootNode rootNode)
        {
            mRootNode = rootNode;
        }

        public void Intercept(IInvocation invocation)
        {
            // TODO: retrieve stacktrace here (first point)
            if (mRootNode.IsReplaying)
                InterceptInReplayMode(invocation);
            else
                InterceptInRecordingMode(invocation);
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.GetCallKeyFromSetter(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in recording mode. Only property setter allowed", invocation.Method);

            mRootNode.Children.Add(new InterfacePropertyNode(invocation, propertyCallKey, this));
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.GetCallKeyFromGetter(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in replay mode. Only property getter allowed", invocation.Method);

            object value;
            if (!mReplayPropertyValues.TryGetValue(propertyCallKey, out value))
                throw new CaseValueNotFoundException("Call to {0} cannot be replayed as it has not been recorded", invocation.Method);

            invocation.ReturnValue = value;
        }

        public void AddReplayValue(PropertyCallKey callKey, object value)
        {
            mReplayPropertyValues[callKey] = value;
        }
    }
}
