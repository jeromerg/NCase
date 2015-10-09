using System;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.RecPlay
{
    public class InvocationRecord : IInvocationRecord
    {
        private readonly string mInvocationTargetName;
        [NotNull] private readonly IInvocation mInvocation;
        [NotNull] private readonly CodeLocation mCodeLocation;

        public InvocationRecord(
            [NotNull] string invocationTargetName,
            [NotNull] IInvocation invocation,
            [NotNull] CodeLocation codeLocation)
        {
            if (invocationTargetName == null) throw new ArgumentNullException("invocationTargetName");
            if (invocation == null) throw new ArgumentNullException("invocation");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mInvocationTargetName = invocationTargetName;
            mInvocation = invocation;
            mCodeLocation = codeLocation;
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public string InvocationTargetName
        {
            get { return mInvocationTargetName; }
        }

        public IInvocation Invocation
        {
            get { return mInvocation; }
        }
    }
}