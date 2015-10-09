﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
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

        private bool mIsReplay;

        public class Factory : IInterfaceReIInterfaceRecPlayNodeFactory
        {
            public IInterfaceRecPlayNode Create(IInterfaceRecPlayInterceptor parentInterceptor,
                                                string contributorName,
                                                PropertyCallKey propertyCallKey,
                                                object propertyValue,
                                                CodeLocation codeLocation)
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

        public CodeLocation CodeLocation
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