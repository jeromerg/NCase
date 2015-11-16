using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.TokenStream;
using NDsl.Back.Api.Util;
using NDsl.Back.Imp.Common;

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
                case TokenStreamMode.None:
                    break;
                case TokenStreamMode.Write:
                    InterceptInRecordingMode(invocation);
                    break;
                case TokenStreamMode.Read:
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

        /// <exception cref="InvalidRecPlayStateException">
        ///     InterfaceRecPlayInterceptor is already in mode '{0}' and cannot be set
        ///     twice
        /// </exception>
        public void AddReplayPropertyValue(PropertyCallKey callKey, object value)
        {
            if (mReplayPropertyValues.ContainsKey(callKey))
                throw new InvalidRecPlayStateException("Property {0} has already been set to replay mode and cannot be set twice", callKey);

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
            // REFUSE CALL TO GETTER
            PropertyCallKey getterPropertyCallKey = invocation.TryGetPropertyCallKeyFromGetter();
            if (getterPropertyCallKey != null)
            {
                string invocationPrint = InterfaceRecPlayNodeExtensions.PrintInvocation(mContributorName, getterPropertyCallKey);
                throw new InvalidRecPlayStateException("Invalid call to getter '{0}' in Recording mode. Only property setter allowed", invocationPrint);
            }

            // REJECT CALL TO ANY NON GETTER 
            PropertyCallKey setterPropertyCallKey = invocation.TryGetPropertyCallKeyFromSetter();
            if (setterPropertyCallKey == null)
            {
                throw new InvalidRecPlayStateException("Invalid call to {0}.{1} in Recording mode. Only property setter allowed", mContributorName, invocation.Method);
            }

            CodeLocation codeLocation = mCodeLocationUtil.GetCurrentUserCodeLocation();
            var invocationRecord = new InvocationRecord(mContributorName, invocation, codeLocation);
            var token = new InvocationToken<IInterfaceRecPlayInterceptor>(this, invocationRecord, codeLocation);
            mTokenWriter.Append(token);
        }

        private void InterceptInReplayMode(IInvocation invocation)
        {
            // REJECT CALL TO SETTER
            PropertyCallKey setterPropertyCallKey = invocation.TryGetPropertyCallKeyFromSetter();
            if (setterPropertyCallKey != null)
            {
                string invocationPrint = InterfaceRecPlayNodeExtensions.PrintInvocation(mContributorName, setterPropertyCallKey);
                throw new InvalidRecPlayStateException("Invalid call to setter '{0}' in Replay mode. Only property getter allowed", invocationPrint);
            }

            // REJECT CALL TO ANY NON GETTER 
            PropertyCallKey getterPropertyCallKey = invocation.TryGetPropertyCallKeyFromGetter();
            if (getterPropertyCallKey == null)
            {
                throw new InvalidRecPlayStateException("Invalid call to {0}.{1} in replay mode. Only property getter allowed", mContributorName, invocation.Method);
            }

            object value;
            if (!mReplayPropertyValues.TryGetValue(getterPropertyCallKey, out value))
            {
                string invocationPrint = InterfaceRecPlayNodeExtensions.PrintInvocation(mContributorName, getterPropertyCallKey);
                throw new CaseValueNotFoundException("Call to getter {0} cannot be replayed as it has not been recorded", invocationPrint);
            }

            invocation.ReturnValue = value;
        }
    }
}