using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NDsl.Back.Imp.RecPlay
{
    public class InterfaceRecPlayNode : IInterfaceRecPlayNode
    {
        #region inner types

        public class Factory : IInterfaceReIInterfaceRecPlayNodeFactory
        {
            public IInterfaceRecPlayNode Create(IInterfaceRecPlayInterceptor parentInterceptor,
                                                string contributorName,
                                                PropertyCallKey propertyCallKey,
                                                object propertyValue,
                                                ICodeLocation codeLocation)
            {
                return new InterfaceRecPlayNode(parentInterceptor, contributorName, propertyCallKey, propertyValue, codeLocation);
            }
        }

        #endregion

        [NotNull] private readonly IInterfaceRecPlayInterceptor mParentInterceptor;

        [NotNull] private readonly string mContributorName;
        [NotNull] private readonly PropertyCallKey mPropertyCallKey;
        [CanBeNull] private readonly object mPropertyValue;
        [NotNull] private readonly ICodeLocation mCodeLocation;

        private bool mIsReplay;

        public InterfaceRecPlayNode([NotNull] IInterfaceRecPlayInterceptor parentInterceptor,
                                    [NotNull] string contributorName,
                                    [NotNull] PropertyCallKey propertyCallKey,
                                    [CanBeNull] object propertyValue,
                                    [NotNull] ICodeLocation codeLocation)
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

        public string ContributorName
        {
            get { return mContributorName; }
        }

        public PropertyCallKey PropertyCallKey
        {
            get { return mPropertyCallKey; }
        }

        public object PropertyValue
        {
            get { return mPropertyValue; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public IEnumerable<INode> Children
        {
            get { yield break; }
        }

        public bool IsReplay
        {
            get { return mIsReplay; }
            set
            {
                if (mIsReplay == value)
                    return;

                mIsReplay = value;

                if (value)
                {
                    mParentInterceptor.SetMode(RecPlayMode.Playing);
                    mParentInterceptor.AddReplayPropertyValue(mPropertyCallKey, mPropertyValue);
                }
                else
                {
                    mParentInterceptor.SetMode(RecPlayMode.Recording);
                    mParentInterceptor.RemoveReplayPropertyValue(mPropertyCallKey);
                }
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