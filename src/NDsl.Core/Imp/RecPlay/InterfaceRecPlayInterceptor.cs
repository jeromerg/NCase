using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NDsl.Util.Castle;
using NVisitor.Common.Quality;

namespace NDsl.Imp.RecPlay
{
    public class InterfaceRecPlayInterceptor : IInterceptor, IInterfaceRecPlayInterceptor
    {
        #region public enum
        public enum Mode { Recording, Playing }
        #endregion

        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        
        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mContributorName;
        [NotNull] private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues = new Dictionary<PropertyCallKey, object>();

        private Mode mMode;

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

        public void SetMode(Mode mode)
        {
            mMode = mode;
        }

        [NotNull]
        public string ContributorName
        {
            get { return mContributorName; }
        }

        public void Intercept(IInvocation invocation)
        {
            switch (mMode)
            {
                case Mode.Recording:
                    InterceptInRecordingMode(invocation);
                    break;

                case Mode.Playing:
                    InterceptInReplayMode(invocation);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
            var token = new InvocationToken<InterfaceRecPlayInterceptor>(this, invocationRecord, codeLocation);
            mTokenWriter.Append(token);
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = InvocationUtil.TryGetPropertyCallKeyFromGetter(invocation);
            if (propertyCallKey == null)
                throw new InvalidCaseRecordException("Invalid call to {0} in replay mode. Only property getter allowed", invocation.Method);

            object value;
            if (!mReplayPropertyValues.TryGetValue(propertyCallKey, out value))
                throw new CaseValueNotFoundException("Call to {0} cannot be replayed as it has not been recorded", invocation.Method);

            invocation.ReturnValue = value;
        }
    }
}
