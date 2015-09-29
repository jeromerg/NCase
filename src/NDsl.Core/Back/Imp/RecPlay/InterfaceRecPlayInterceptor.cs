using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayInterceptor : IInterceptor, IInterfaceRecPlayInterceptor
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mContributorName;

        [NotNull] private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues =
            new Dictionary<PropertyCallKey, object>();

        private RecPlayMode mMode;

        public InterfaceRecPlayInterceptor(
            [NotNull] ITokenWriter tokenWriter,
            [NotNull] string contributorName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (contributorName == null) throw new ArgumentNullException("contributorName");

            mCodeLocationUtil = codeLocationUtil;
            mContributorName = contributorName;
            mTokenWriter = tokenWriter;
        }

        public void Intercept(IInvocation invocation)
        {
            switch (mMode)
            {
                case RecPlayMode.Recording:
                    InterceptInRecordingMode(invocation);
                    break;

                case RecPlayMode.Playing:
                    InterceptInReplayMode(invocation);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull] public string ContributorName
        {
            get { return mContributorName; }
        }

        public void SetMode(RecPlayMode mode)
        {
            mMode = mode;
        }

        public void AddReplayPropertyValue(PropertyCallKey callKey, object value)
        {
            mReplayPropertyValues[callKey] = value;
        }

        public void RemoveReplayPropertyValue(PropertyCallKey callKey)
        {
            mReplayPropertyValues.Remove(callKey);
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            ICodeLocation codeLocation = mCodeLocationUtil.GetCurrentUserCodeLocation();
            var invocationRecord = new InvocationRecord(mContributorName, invocation, codeLocation);
            var token = new InvocationToken<IInterfaceRecPlayInterceptor>(this, invocationRecord, codeLocation);
            mTokenWriter.Append(token);
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.TryGetPropertyCallKeyFromGetter(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in replay mode. Only property getter allowed",
                                                     invocation.Method);

            object value;
            if (!mReplayPropertyValues.TryGetValue(propertyCallKey, out value))
                throw new CaseValueNotFoundException("Call to {0} cannot be replayed as it has not been recorded",
                                                     invocation.Method);

            invocation.ReturnValue = value;
        }
    }
}