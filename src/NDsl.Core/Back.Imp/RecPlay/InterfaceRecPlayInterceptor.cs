using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Record;
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
            switch (mTokenWriter.Mode)
            {
                case RecorderMode.None:
                    throw BuildEx(invocation, "Call to '{0}' is performed outside any record or replay block");

                case RecorderMode.Write:
                    InterceptInRecordingMode(invocation);
                    break;

                case RecorderMode.Read:
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

        /// <exception cref="InvalidRecPlayStateException"/>
        public void AddReplayPropertyValue([NotNull] PropertyCallKey callKey, object value)
        {
            if (mReplayPropertyValues.ContainsKey(callKey))
                throw BuildEx(callKey, "'{0}' cannot be recorded twice");

            mReplayPropertyValues[callKey] = value;
        }

        /// <exception cref="InvalidRecPlayStateException"/>
        public void RemoveReplayPropertyValue([NotNull] PropertyCallKey callKey)
        {
            if (!mReplayPropertyValues.ContainsKey(callKey))
                BuildEx(callKey, "No replay found for '{0}'");

            mReplayPropertyValues.Remove(callKey);
        }

        private void InterceptInRecordingMode(IInvocation invocation)
        {
            PropertyCallKey setterPropertyCallKey = invocation.TryGetPropertyCallKeyFromSetter();
            if (setterPropertyCallKey == null)
                throw BuildEx(invocation, "Call to '{0}' is not allowed inside recording block: Only call to setter are allowed");

            CodeLocation codeLocation = mCodeLocationUtil.GetCurrentUserCodeLocation();
            var invocationRecord = new InvocationRecord(mContributorName, invocation, codeLocation);
            var token = new InvocationToken<IInterfaceRecPlayInterceptor>(this, invocationRecord, codeLocation);
            mTokenWriter.Append(token);
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            PropertyCallKey getterPropertyCallKey = invocation.TryGetPropertyCallKeyFromGetter();
            if (getterPropertyCallKey == null)
                throw BuildEx(invocation, "Call to '{0}' is not allowed inside replay block: Only call to getter are allowed");

            object value;
            if (!mReplayPropertyValues.TryGetValue(getterPropertyCallKey, out value))
                throw BuildEx(getterPropertyCallKey, "Call to getter {0}: It cannot be replayed as it has not been recorded");

            invocation.ReturnValue = value;
        }

        /// <exception cref="InvalidRecPlayStateException"/>
        private InvalidRecPlayStateException BuildEx(IInvocation invocation, string format)
        {
            PropertyCallKey propertyCallKey = invocation.TryGetPropertyCallKeyFromGetter()
                                              ?? invocation.TryGetPropertyCallKeyFromSetter();
            if (propertyCallKey != null)
            {
                return BuildEx(propertyCallKey, format);
            }
            else
            {
                return new InvalidRecPlayStateException(format, mContributorName, invocation.Method);
            }
        }

        /// <exception cref="InvalidRecPlayStateException"/>
        private InvalidRecPlayStateException BuildEx(PropertyCallKey propertyCallKey, string format)
        {
            string msg = string.Format(format, PrintInvocation(propertyCallKey));
            return new InvalidRecPlayStateException(msg);
        }

        private string PrintInvocation(PropertyCallKey getterPropertyCallKey)
        {
            return InterfaceRecPlayNodeExtensions.PrintInvocation(mContributorName, getterPropertyCallKey);
        }
    }
}