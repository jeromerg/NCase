using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayInterceptor : IInterceptor, IInterfaceRecPlayInterceptor
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mContributorName;

        [NotNull] private readonly Dictionary<PropertyCallKey, object> mReplayPropertyValues =
            new Dictionary<PropertyCallKey, object>();

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
            if (mReplayPropertyValues.Any())
            {
                InterceptInReplayMode(invocation);
            }
            else
            {
                InterceptInRecordingMode(invocation);
            }
        }

        [NotNull] public string ContributorName
        {
            get { return mContributorName; }
        }

        /// <exception cref="InvalidRecPlayStateException">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and cannot be set
        ///     twice
        /// </exception>
        public void AddReplayPropertyValue(PropertyCallKey callKey, object value)
        {
            if (mReplayPropertyValues.ContainsKey(callKey))
                throw new InvalidRecPlayStateException(
                    "Property {0} has already been set to replay mode and cannot be set twice",
                    callKey);

            mReplayPropertyValues[callKey] = value;
        }

        /// <exception cref="InvalidRecPlayStateException">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and cannot be set
        ///     twice
        /// </exception>
        public void RemoveReplayPropertyValue(PropertyCallKey callKey)
        {
            if (!mReplayPropertyValues.ContainsKey(callKey))
                throw new InvalidRecPlayStateException("Property {0} was not set to replay mode", callKey);

            mReplayPropertyValues.Remove(callKey);
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            CodeLocation codeLocation = mCodeLocationUtil.GetCurrentUserCodeLocation();
            var invocationRecord = new InvocationRecord(mContributorName, invocation, codeLocation);
            var token = new InvocationToken<IInterfaceRecPlayInterceptor>(this, invocationRecord, codeLocation);
            mTokenWriter.Append(token);
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey propertyCallKey = invocation.TryGetPropertyCallKeyFromGetter();
            if (propertyCallKey == null)
                throw new InvalidRecPlayStateException("Invalid call to {0} in replay mode. Only property getter allowed",
                                                       invocation.Method);

            object value;
            if (!mReplayPropertyValues.TryGetValue(propertyCallKey, out value))
                throw new CaseValueNotFoundException("Call to {0} cannot be replayed as it has not been recorded",
                                                     invocation.Method);

            invocation.ReturnValue = value;
        }
    }
}