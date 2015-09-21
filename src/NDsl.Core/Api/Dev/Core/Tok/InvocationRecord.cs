using System;
using Castle.DynamicProxy;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NDsl.Api.Dev.Core.Tok
{
    public class InvocationRecord : IInvocationRecord
    {
        private readonly string mInvocationTargetName;
        [NotNull] private readonly IInvocation mInvocation;
        [NotNull] private readonly ICodeLocation mCodeLocation;

        public InvocationRecord(
            [NotNull] string invocationTargetName,
            [NotNull] IInvocation invocation,
            [NotNull] ICodeLocation codeLocation)
        {
            if (invocationTargetName == null) throw new ArgumentNullException("invocationTargetName");
            if (invocation == null) throw new ArgumentNullException("invocation");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mInvocationTargetName = invocationTargetName;
            mInvocation = invocation;
            mCodeLocation = codeLocation;
        }

        public string InvocationTargetName
        {
            get { return mInvocationTargetName; }
        }

        public IInvocation Invocation
        {
            get { return mInvocation; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}