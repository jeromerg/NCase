using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayNode : IInterfaceRecPlayNode
    {
        [NotNull] private readonly IInterfaceRecPlayInterceptor mParentInterceptor;
        [NotNull] private readonly string mContributorName;
        [NotNull] private readonly PropertyCallKey mPropertyCallKey;
        [CanBeNull] private readonly object mPropertyValue;
        [NotNull] private readonly CodeLocation mCodeLocation;

        public class Factory : IInterfaceReIInterfaceRecPlayNodeFactory
        {
            public IInterfaceRecPlayNode Create([NotNull] IInterfaceRecPlayInterceptor parentInterceptor,
                                                [NotNull] string contributorName,
                                                [NotNull] PropertyCallKey propertyCallKey,
                                                [CanBeNull] object propertyValue,
                                                [NotNull] CodeLocation codeLocation)
            {
                return new InterfaceRecPlayNode(parentInterceptor, contributorName, propertyCallKey, propertyValue, codeLocation);
            }
        }

        public InterfaceRecPlayNode([NotNull] IInterfaceRecPlayInterceptor parentInterceptor,
                                    [NotNull] string contributorName,
                                    [NotNull] PropertyCallKey propertyCallKey,
                                    [CanBeNull] object propertyValue,
                                    [NotNull] CodeLocation codeLocation)
        {
            if (parentInterceptor == null) throw new ArgumentNullException("parentInterceptor");
            if (contributorName == null) throw new ArgumentNullException("contributorName");
            if (propertyCallKey == null) throw new ArgumentNullException("propertyCallKey");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mPropertyValue = propertyValue;
            mContributorName = contributorName;
            mParentInterceptor = parentInterceptor;
            mCodeLocation = codeLocation;
            mPropertyCallKey = propertyCallKey;
        }

        [NotNull] public string ContributorName
        {
            get { return mContributorName; }
        }

        [NotNull] public PropertyCallKey PropertyCallKey
        {
            get { return mPropertyCallKey; }
        }

        [CanBeNull] public object PropertyValue
        {
            get { return mPropertyValue; }
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        [NotNull, ItemNotNull] public IEnumerable<INode> Children
        {
            get { yield break; }
        }

        public void SetReplay(bool value)
        {
            try
            {
                if (value)
                {
                    mParentInterceptor.AddReplayPropertyValue(mPropertyCallKey, mPropertyValue);
                }
                else
                {
                    mParentInterceptor.RemoveReplayPropertyValue(mPropertyCallKey);
                }
            }
            catch (InvalidRecPlayStateException e)
            {
                // add statement location
                throw new InvalidRecPlayStateException(e,
                                                       "{0}: {1}",
                                                       e.Message,
                                                       mCodeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
            }
        }

        public override string ToString()
        {
            return string.Format("ContributorName: {0}, PropertyCallKey: {1}, PropertyValue: {2}",
                                 mContributorName,
                                 mPropertyCallKey,
                                 mPropertyValue);
        }
    }
}