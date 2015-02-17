using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NDsl.Imp.Core.Token;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Impl.RecPlay
{
    public class RecPlay : IInterceptor, IRecPlayInterfaceInterceptor
    {
        [NotNull] private readonly IAstRoot mAstRoot;
        private readonly string mContributorName;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues = new Dictionary<PropertyCallKey, object>();

        public RecPlay(
            [NotNull] IAstRoot astRoot, 
            [NotNull] string contributorName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (astRoot == null) throw new ArgumentNullException("astRoot");
            if (contributorName == null) throw new ArgumentNullException("contributorName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mAstRoot = astRoot;
            mContributorName = contributorName;
            mCodeLocationUtil = codeLocationUtil;
        }

        public void Intercept(IInvocation invocation)
        {
            switch (mAstRoot.State)
            {
                case AstState.Writing:
                    InterceptInRecordingMode(invocation);
                    break;

                case AstState.Processing:
                    throw new DslInvalidStateException("Invocation of RecPlay Contributors can occur only in Writing or Reading state");
                
                case AstState.Reading:
                    InterceptInReplayMode(invocation);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            ICodeLocation codeLocation = mCodeLocationUtil.GetUserCodeLocation();
            var token = new InvocationToken<RecPlay>(this, new InvocationRecord(mContributorName, invocation, codeLocation));
            mAstRoot.AddChild(token);
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

        public void AddReplayPropertyValue(PropertyCallKey callKey, object value)
        {
            mReplayPropertyValues[callKey] = value;
        }
    }
}
