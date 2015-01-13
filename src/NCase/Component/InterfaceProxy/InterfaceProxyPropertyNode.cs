using Castle.DynamicProxy;
using NTestCase.Base;
using NTestCase.Util.Stacktrace;

namespace NTestCase.Component.InterfaceProxy
{
    public class InterfaceProxyPropertyNode : NodeBase<ComponentAndMethodInfo>
    {
        private readonly InvocationAndFrame mInvocationAndFrame;
 
        public InterfaceProxyPropertyNode(IInvocation invocation)
            : base(BuildTargetKey(invocation))
        {
            mInvocationAndFrame = new InvocationAndFrame(invocation);
        }

        public InvocationAndFrame InvocationAndFrame
        {
            get { return mInvocationAndFrame; }
        }

        public override void Replay()
        {
            throw new System.NotImplementedException("TODO");
        }

        private static ComponentAndMethodInfo BuildTargetKey(IInvocation invocation)
        {
            return new ComponentAndMethodInfo(invocation.InvocationTarget, invocation.Method);
        }
    }
}
