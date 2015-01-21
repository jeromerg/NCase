using System.Diagnostics;
using Castle.DynamicProxy;
using NCase.Api.Dev;
using NCase.Util;
using NCase.Util.Castle;
using NCase.Util.Quality;

namespace NCase.Core.InterfaceContrib
{
    public class InterfacePropertyNode : INode
    {
        private readonly IInvocation mInvocation;
        private readonly IReplayInterceptor mReplayInterceptor;
        private readonly StackFrame mStackFrame;
        [NotNull]
        private readonly PropertyCallKey mPropertyCallKey;

        public InterfacePropertyNode(IInvocation invocation, PropertyCallKey propertyCallKey, IReplayInterceptor replayInterceptor)
        {
            mInvocation = invocation;
            mReplayInterceptor = replayInterceptor;
            mStackFrame = StackFrameUtil.GetOuterStackFrame();
            mPropertyCallKey = propertyCallKey;
        }

        public IInvocation Invocation
        {
            get { return mInvocation; }
        }

        public StackFrame StackFrame
        {
            get { return mStackFrame; }
        }

        public PropertyCallKey PropertyCallKey
        {
            get { return mPropertyCallKey; }
        }

        public void Replay()
        {
            mReplayInterceptor.Replay(mPropertyCallKey, mInvocation.GetArgumentValue(0));
        }
    }
}